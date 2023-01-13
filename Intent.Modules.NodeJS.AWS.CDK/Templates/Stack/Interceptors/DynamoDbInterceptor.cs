using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common;
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
                var options = string.Concat(Options(resource).Select(x => $"{Environment.NewLine}            {x},"));

                constructor.AddStatement($@"const {variableName} = new dynamodb.Table(this, '{resource.Name}', {{{options}
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

            IEnumerable<string> Options(IElement element)
            {
                var partitionKey = element.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.PartitionKey);
                if (partitionKey != null)
                {
                    yield return $"partitionKey: {Attribute(partitionKey)}";
                }

                var sortKey = element.ChildElements.SingleOrDefault(x => x.SpecializationType == Constants.ElementName.SortKey);
                if (sortKey != null)
                {
                    yield return $"sortKey: {Attribute(sortKey)}";
                }

                var removalPolicy = element.GetStereotypeProperty<string>(
                    Constants.Stereotype.DynamoDbTableSettings.Name,
                    Constants.Stereotype.DynamoDbTableSettings.Property.RemovalPolicy);
                if (!string.IsNullOrWhiteSpace(removalPolicy))
                {
                    constructor.Class.File.AddImport("RemovalPolicy", "aws-cdk-lib");
                    yield return $"removalPolicy: RemovalPolicy.{removalPolicy.ToUpperInvariant()}";
                }
            }

            static string Attribute(IElement element)
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

        public void ApplyPost(TypescriptConstructor constructor)
        {
        }
    }
}
