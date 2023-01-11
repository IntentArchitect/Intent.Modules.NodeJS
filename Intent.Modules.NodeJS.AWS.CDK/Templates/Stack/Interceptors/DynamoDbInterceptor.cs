using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class DynamoDbInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _stackTemplate;

        public DynamoDbInterceptor(StackTemplate stackTemplate)
        {
            _stackTemplate = stackTemplate;
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _stackTemplate.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.DynamoDbTable)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("*", "dynamodb", "aws-cdk-lib/aws-dynamodb");
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.ToCamelCase()}DynamoDbTable";
                var environmentVariableName = variableName.ToSnakeCase().ToUpperInvariant();
                var partitionKeyElement = resource.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.PartitionKey);
                var partitionKey = partitionKeyElement != null
                    ? $"{Environment.NewLine}            partitionKey: {GetAttribute(partitionKeyElement)},"
                    : string.Empty;
                var sortKeyElement = resource.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.SortKey);
                var sortKey = sortKeyElement != null
                    ? $"{Environment.NewLine}            sortKey: {GetAttribute(sortKeyElement)},"
                    : string.Empty;

                constructor.AddStatement($@"const {variableName} = new dynamodb.Table(this, '{resource.Name}', {{{partitionKey}{sortKey}
            removalPolicy: RemovalPolicy.DESTROY,
        }});", statement =>
                {
                    statement
                        .SeparatedFromPrevious()
                        .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                        .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                        .AddMetadata(Constants.MetadataKey.DynamoDbTableName, environmentVariableName)
                        .AddMetadata(Constants.MetadataKey.EnvironmentVariables, new Dictionary<string, string>
                        {
                            [environmentVariableName] = $"{variableName}.tableName"
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
