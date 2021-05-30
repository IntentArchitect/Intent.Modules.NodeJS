using Intent.Engine;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Modules.Common.Templates;
using System.Collections.Generic;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.Main
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class MainTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Core.Main";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public MainTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        public string AppModuleClass => GetTypeName(AppModule.AppModuleTemplate.TemplateId);

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"Main",
                fileName: $"main"
            );
        }

    }
}