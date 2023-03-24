using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Schema
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class SchemaTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            var result = $@"export const {ClassName} = {{
  type: ""object"",
  properties: {{{string.Join(',', GetProperties())}
  }},
  required: [{string.Join(", ", GetRequired())}]
}};
";
            return result;
        }

        private IEnumerable<string> GetProperties()
        {
            return Model.Fields
                .Select(field => @$"
    {field.Name.ToCamelCase()}: {GetProperty(field)}");
        }

        private string GetProperty(IHasTypeReference field)
        {
            if (field.TypeReference.Element.IsMessageModel())
            {
                var schemaName = this.GetSchemaName(new MessageModel((IElement)field.TypeReference.Element));

                return !field.TypeReference.IsCollection
                    ? schemaName
                    : @$"{{
        type: 'array',
        items: {schemaName}
    }}";
            }

            var fields = field.TypeReference.Element.Name switch
            {
                "binary" => new Dictionary<string, string>
                {
                    ["type"] = "'string'",
                    ["contentEncoding"] = "'base64'",
                    ["contentMediaType"] = "'application/octet-stream'"
                },
                "bool" => new Dictionary<string, string>
                {
                    ["type"] = "'boolean'"
                },
                "byte" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "char" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                "date" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                "datetime" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                "datetimeoffset" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                "decimal" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "double" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "float" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "guid" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                "int" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "long" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "object" => new Dictionary<string, string>
                {
                    ["type"] = "'object'"
                },
                "short" => new Dictionary<string, string>
                {
                    ["type"] = "'number'"
                },
                "string" => new Dictionary<string, string>
                {
                    ["type"] = "'string'"
                },
                _ => new Dictionary<string, string>
                {
                    ["type"] = "'object'"
                }
            };

            if (field.TypeReference.IsCollection)
            {
                return @$"{{
        type: {(field.TypeReference.IsNullable ? "['array', 'null']" : "'array'")},
        items: {{{string.Join(',', fields.Select(item => $@"
            {item.Key}: {item.Value}"))}
        }}
    }}";
            }

            if (field.TypeReference.IsNullable)
            {
                fields["type"] = $"[{fields["type"]}, 'null']";
            }

            return @$"{{{string.Join(',', fields.Select(item => @$"
        {item.Key}: {item.Value}"))}
    }}";
        }

        private IEnumerable<string> GetRequired()
        {
            return Model.Fields
                .Where(field => !field.TypeReference.IsNullable)
                .Select(field => $"'{field.Name.ToCamelCase()}'");
        }
    }
}