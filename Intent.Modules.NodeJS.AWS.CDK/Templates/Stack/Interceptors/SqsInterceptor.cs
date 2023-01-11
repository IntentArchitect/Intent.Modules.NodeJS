using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class SqsInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _stackTemplate;

        public SqsInterceptor(StackTemplate stackTemplate)
        {
            _stackTemplate = stackTemplate;
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _stackTemplate.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.SqsQueue)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("*", "sqs", "aws-cdk-lib/aws-sqs");
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.ToCamelCase().RemoveSuffix("Sqs", "Queue")}SqsQueue";
                var environmentVariableName = $"{variableName}Url".ToSnakeCase().ToUpperInvariant();

                constructor.AddStatement($@"const {variableName} = new sqs.Queue(this, '{resource.Name}');", statement =>
                {
                    statement
                        .SeparatedFromPrevious()
                        .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                        .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                        .AddMetadata(Constants.MetadataKey.SqsQueueUrl, environmentVariableName)
                        .AddMetadata(Constants.MetadataKey.EnvironmentVariables, new Dictionary<string, string>
                        {
                            [environmentVariableName] = $"{variableName}.queueUrl"
                        });
                });
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
        }

        private static string GetAttribute(IElement element)
        {
            var attributeType = element.TypeReference.Element?.Name switch
            {
                "binary" => "BINARY",
                "byte" => "NUMBER",
                "char" => "STRING",
                "date" => "STRING",
                "datetime" => "STRING",
                "datetimeoffset" => "STRING",
                "decimal" => "NUMBER",
                "double" => "NUMBER",
                "float" => "NUMBER",
                "guid" => "STRING",
                "int" => "NUMBER",
                "long" => "NUMBER",
                "short" => "NUMBER",
                "string" => "STRING",
                _ => throw new ArgumentOutOfRangeException(nameof(element.TypeReference), element.TypeReference.Element?.Name, null)
            };

            var name = element.Name.ToCamelCase();

            return $"{{ name: '{name}', type: dynamodb.AttributeType.{attributeType} }}";
        }
    }
}
