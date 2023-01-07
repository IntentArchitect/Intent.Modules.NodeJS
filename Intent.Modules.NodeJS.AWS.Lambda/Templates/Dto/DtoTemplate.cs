using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Dto
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class DtoTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} {{{string.Concat(GetMembers())}
}}
";
        }

        private IEnumerable<string> GetMembers()
        {
            foreach (var field in Model.Fields)
            {
                yield return @$"
    {field.Name.ToCamelCase()}{(field.TypeReference.IsNullable ? "?" : string.Empty)}: {GetTypeName(field)};";
            }

            foreach (var member in GetDecorators().SelectMany(x => x.GetMembers()))
            {
                yield return member;
            }
        }
    }
}