using System.Collections.Generic;
using Intent.Engine;
using Intent.Eventing;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Winston.Templates.WinstonOptions
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class WinstonOptionsTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NestJS.Winston.WinstonOptions";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public WinstonOptionsTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(new NpmPackageDependency("nest-winston", "^1.7.0"));
            AddDependency(new NpmPackageDependency("winston", "^3.8.1"));
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();
            ExecutionContext.EventDispatcher.Publish(new NestApplicationOptionRequest(
                name: "logger",
                value: "WinstonModule.createLogger(WinstonOptions)",
                typesToImport: new[]
                {
                    ("WinstonModule", "nest-winston"),
                    ("WinstonOptions", "./winston.config")
                }));

            ExecutionContext.EventDispatcher.Publish(new NestJsProviderRequest(
                type: "Logger",
                importFromLocation: "@nestjs/common"));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"WinstonOptions",
                fileName: $"winston.config",
                overwriteBehaviour: OverwriteBehaviour.OnceOff
            );
        }

    }
}