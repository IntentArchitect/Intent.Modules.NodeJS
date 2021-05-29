using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.AppModule
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class AppModuleTemplate : TypeScriptTemplateBase<object>
    {
        private readonly IList<string> _controllers = new List<string>();
        private readonly IList<string> _providers = new List<string>();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Core.AppModule";

        public AppModuleTemplate(IOutputTarget outputTarget, object model) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<NestJsControllerCreatedEvent>(HandleEvent);
            ExecutionContext.EventDispatcher.Subscribe<NestJsProviderCreatedEvent>(HandleEvent);
        }

        private void HandleEvent(NestJsControllerCreatedEvent @event)
        {
            _controllers.Add(GetTypeName(@event.TemplateId, @event.ModelId));
        }

        private void HandleEvent(NestJsProviderCreatedEvent @event)
        {
            _providers.Add(GetTypeName(@event.TemplateId, @event.ModelId));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: "AppModule",
                fileName: "app.module"
            );
        }

        private string GetModuleImports()
        {
            return "";
        }

        private string GetControllers()
        {
            return _controllers.Any() ? @"
    " + string.Join(@",
    ", _controllers) + @"
  " : "";
        }

        private string GetProviders()
        {
            return _providers.Any() ? @"
    " + string.Join(@",
    ", _providers) + @"
  " : "";
        }
    }
}