using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NextJS.Core.Templates.AppModule
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class AppModule : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NextJS.Core.Templates.AppModule";

        public AppModule(IOutputTarget outputTarget, object model) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe();
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig DefineDefaultFileMetadata()
        {
            return new TypeScriptDefaultFileMetadata(
                overwriteBehaviour: OverwriteBehaviour.Always,
                fileName: "AppModule",
                relativeLocation: "",
                className: "AppModule"
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