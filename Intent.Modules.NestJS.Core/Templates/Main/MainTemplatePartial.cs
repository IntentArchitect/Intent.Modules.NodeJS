using System;
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

namespace Intent.Modules.NestJS.Core.Templates.Main
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class MainTemplate : TypeScriptTemplateBase<object>
    {
        private readonly Dictionary<string, string> _nestApplicationOptions = new();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Core.Main";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public MainTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<NestApplicationOptionRequest>(RegisterNestApplicationOption);
        }

        private void RegisterNestApplicationOption(NestApplicationOptionRequest @event)
        {
            if (_nestApplicationOptions.ContainsKey(@event.Name))
            {
                throw new Exception($"More than one attempt to register the \"{@event.Name}\" registration option.");
            }

            foreach (var (type, location) in @event.TypesToImport)
            {
                AddImport(type, location);
            }

            _nestApplicationOptions[@event.Name] = @event.Value;
        }

        private string GetApplicationOptions()
        {
            if (_nestApplicationOptions.Count == 0)
            {
                return string.Empty;
            }

            var options = _nestApplicationOptions.Select(item => $"{item.Key}: {item.Value}");

            return $@", {{
    {string.Join($",{Environment.NewLine}    ", options)}
  }}";
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