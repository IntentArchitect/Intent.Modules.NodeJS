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
        private readonly IList<string> _imports = new List<string>();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Core.AppModule";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public AppModuleTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<NestJsModuleImportRequest>(HandleEvent);
            ExecutionContext.EventDispatcher.Subscribe<NestJsControllerCreatedEvent>(HandleEvent);
            ExecutionContext.EventDispatcher.Subscribe<NestJsProviderCreatedEvent>(HandleEvent);
            ExecutionContext.EventDispatcher.Subscribe<NestJsProviderRequest>(HandleEvent);
        }

        private void HandleEvent(NestJsProviderRequest @event)
        {
            AddImport(@event.Type, @event.ImportFromLocation);

            _providers.Add(@event.Type);
        }

        private void HandleEvent(NestJsModuleImportRequest @event)
        {
            foreach (var import in @event.Imports)
            {
                AddImport(import.Type, import.Location);
            }

            foreach (var dependency in @event.Dependencies)
            {
                AddTemplateDependency(dependency);
            }

            _imports.Add(@event.Statement);
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
            return _imports.Any() ? @"
    " + string.Join(@",
    ", _imports) + @"
  " : "";
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