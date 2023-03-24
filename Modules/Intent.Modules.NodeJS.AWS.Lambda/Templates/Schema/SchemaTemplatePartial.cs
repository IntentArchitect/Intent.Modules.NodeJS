using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Schema
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class SchemaTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.Api.MessageModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Schema";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public SchemaTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Api.MessageModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = $"{Model.Name.ToCamelCase()}Schema";

            return new TypeScriptFileConfig(
                className: className.ToPascalCase(),
                fileName: className.Replace("DTO", "Dto").ToKebabCase(),
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package)
            );
        }

    }
}