using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.Types.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Module.TypeScript.Domain.Templates.Enum
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EnumTemplate : TypeScriptTemplateBase<Intent.Modules.Common.Types.Api.EnumModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.Domain.Enum";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EnumTemplate(IOutputTarget outputTarget, Intent.Modules.Common.Types.Api.EnumModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.ToKebabCase()}.enum",
                relativeLocation: this.GetFolderPath());
        }
        private static string GetEnumLiterals(IEnumerable<EnumLiteralModel> literals)
        {
            return string.Join(@",
  ", literals.Select(GetEnumLiteral));
        }

        private static string GetEnumLiteral(EnumLiteralModel literal)
        {
            return $"{literal.Name}{(string.IsNullOrWhiteSpace(literal.Value) ? "" : $" = {literal.Value}")}";
        }
    }
}