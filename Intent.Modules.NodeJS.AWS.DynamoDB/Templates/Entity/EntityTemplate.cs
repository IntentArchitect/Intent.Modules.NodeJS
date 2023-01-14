using System;
using System.Collections.Generic;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.Entity
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class EntityTemplate
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
            foreach (var model in Model.Attributes)
            {
                yield return @$"
    {model.Name.ToCamelCase()}{Optionality(model.TypeReference)}: {GetTypeName(model.TypeReference)}";
            }

            foreach (var model in Model.AssociatedToClasses())
            {
                yield return @$"
    {model.Name.ToCamelCase()}{Optionality(model.TypeReference)}: {GetTypeName(model.TypeReference)}";
            }

            static string Optionality(ITypeReference x) => x.IsNullable ? "?" : string.Empty;
        }
    }
}