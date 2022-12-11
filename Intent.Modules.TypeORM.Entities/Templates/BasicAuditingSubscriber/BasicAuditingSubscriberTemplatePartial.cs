using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.BasicAuditingSubscriber
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class BasicAuditingSubscriberTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.BasicAuditingSubscriber";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public BasicAuditingSubscriberTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.NestJsCls);
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest.Create(null, @"ClsModule.forRoot({
      global: true,
      middleware: { mount: true },
    })")
                .AddImport("ClsModule", "nestjs-cls"));
            ExecutionContext.EventDispatcher.Publish(new NestJsProviderCreatedEvent(moduleId: null, templateId: TemplateId, modelId: null));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"BasicAuditingSubscriber",
                fileName: $"basic-auditing-subscriber"
            );
        }

    }
}