using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.AppModule
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class AppModuleTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NestJS.Core.AppModule";

        public AppModuleTemplate(IOutputTarget outputTarget, object model) : base(TemplateId, outputTarget, model)
        {
            //ExecutionContext.EventDispatcher.Subscribe();
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: "AppModule",
                fileName: "app.module"
            );
        }
    }

    public class NestJsProviderCreatedEvent
    {
        public string ModuleId { get; set; }
        public string TemplateId { get; set; }
        public string ModelId { get; set; }
    }

    public class NestJsControllerCreatedEvent
    {
        public string ModuleId { get; set; }
        public string TemplateId { get; set; }
        public string ModelId { get; set; }
    }
}