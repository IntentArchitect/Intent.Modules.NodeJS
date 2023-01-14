using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;

namespace Intent.Modules.NodeJS.AWS.DynamoDB
{
    internal static class ExtensionMethods
    {
        public static string GetKey(this DynamoDBTableModel model)
        {
            var fields = GetKeys(model).Select(x => $"{x.Name}: {x.TypeScriptType}");

            return $"{{ {string.Join(", ", fields)} }}";
        }

        public static IEnumerable<(string Name, string AttributeType, string KeyType, string TypeScriptType)> GetKeys(this DynamoDBTableModel model)
        {
            var partitionKey = model.PartitionKey;
            if (partitionKey != null)
            {
                yield return (partitionKey.Name.ToCamelCase(), AttributeValueType(partitionKey.TypeReference), "HASH", TypeScriptType(partitionKey.TypeReference));
            }

            var sortKey = model.SortKey;
            if (sortKey != null)
            {
                yield return (sortKey.Name.ToCamelCase(), AttributeValueType(sortKey.TypeReference), "RANGE", TypeScriptType(sortKey.TypeReference));
            }

            static string TypeScriptType(ITypeReference typeReference)
            {
                return AttributeValueType(typeReference) switch
                {
                    "BOOL" => "boolean",
                    "N" => "number",
                    "S" => "string",
                    _ => throw new ArgumentOutOfRangeException(nameof(typeReference), AttributeValueType(typeReference), null)
                };
            }

            static string AttributeValueType(ITypeReference typeReference)
            {
                // https://docs.aws.amazon.com/amazondynamodb/latest/APIReference/API_PutItem.html#API_PutItem_RequestSyntax
                return typeReference.Element?.Name switch
                {
                    "bool" => "BOOL",
                    "byte" => "N",
                    "char" => "S",
                    "date" => "S",
                    "datetime" => "S",
                    "datetimeoffset" => "S",
                    "decimal" => "N",
                    "double" => "N",
                    "float" => "N",
                    "guid" => "S",
                    "int" => "N",
                    "long" => "N",
                    "short" => "N",
                    "string" => "S",
                    _ => throw new ArgumentOutOfRangeException(nameof(typeReference), typeReference.Element?.Name, null)
                };
            }
        }

        public static DynamoDBTableModel GetTable(this EntityModel model) => new(model.InternalElement.ParentElement);
        public static DynamoDBTableModel GetTable(this AttributeModel model)
        {
            var element = model.InternalElement.ParentElement;
            while (element != null)
            {
                if (element.IsDynamoDBTableModel())
                {
                    return new DynamoDBTableModel(element);
                }

                element = element.ParentElement;
            }

            throw new Exception($"Could not find table model for '{model.Name}' [{model.Id}]");
        }
    }
}
