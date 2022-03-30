using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.OrmConfig
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class OrmConfigTemplate : TypeScriptTemplateBase<IList<Intent.Modelers.Domain.Api.ClassModel>>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.OrmConfig";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public OrmConfigTemplate(IOutputTarget outputTarget, IList<Intent.Modelers.Domain.Api.ClassModel> model) : base(TemplateId, outputTarget, model)
        {
            AddDependency(new NpmPackageDependency("typeorm", "^0.2.32"));
            AddDependency(new NpmPackageDependency("@nestjs/typeorm", "^7.1.5"));
            AddDependency(new NpmPackageDependency("sqlite3", "^5.0.2"));
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();
            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest.Create(null, "TypeOrmModule.forRoot(ormconfig)")
                .AddImport("TypeOrmModule", "@nestjs/typeorm")
                .AddDependency(TemplateDependency.OnTemplate(this)));

            var repositories = Model.Select(x => GetTemplate<RepositoryTemplate.RepositoryTemplate>(RepositoryTemplate.RepositoryTemplate.TemplateId, x, new TemplateDiscoveryOptions() { TrackDependency = false })).ToList();

            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest.Create(null, $"TypeOrmModule.forFeature([{string.Join(", ", repositories.Select(x => x.ClassName))}])")
                .AddImport("TypeOrmModule", "@nestjs/typeorm")
                .AddDependencies(repositories.Select(TemplateDependency.OnTemplate).ToArray()));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"ormconfig",
                fileName: $"orm.config"
            );
        }

    }
}