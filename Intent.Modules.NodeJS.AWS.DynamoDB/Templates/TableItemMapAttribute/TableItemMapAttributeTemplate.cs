using System;
using System.Collections.Generic;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemMapAttribute
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class TableItemMapAttributeTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export interface {ClassName} {{{string.Concat(GetMembers())}
}}
";
        }

        private IEnumerable<string> GetMembers()
        {
            foreach (var model in Model.ScalarAttributes)
            {
                yield return @$"
    {model.Name.ToCamelCase()}: {GetTypeName(model.TypeReference)}";
            }

            foreach (var model in Model.DynamoDbItemAssociationTargetEnd())
            {
                yield return @$"
    {model.Name}: {GetTypeName(model.TypeReference)}";
            }
        }
    }
}