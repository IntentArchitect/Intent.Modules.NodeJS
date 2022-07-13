using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.Modules.TypeORM.Entities.DatabaseProviders;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.OrmConfig
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class OrmConfigTemplate : TypeScriptTemplateBase<IList<ClassModel>>
    {
        private readonly IOrmDatabaseProviderStrategy _ormDatabaseProviderStrategy;

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.OrmConfig";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public OrmConfigTemplate(IOutputTarget outputTarget, IList<ClassModel> model) : base(TemplateId, outputTarget, model)
        {
            _ormDatabaseProviderStrategy = IOrmDatabaseProviderStrategy.Resolve(outputTarget);

            AddDependency(new NpmPackageDependency("typeorm", "^0.2.32"));
            AddDependency(new NpmPackageDependency("@nestjs/typeorm", "^7.1.5"));

            foreach (var dependency in _ormDatabaseProviderStrategy.GetPackageDependencies())
            {
                AddDependency(dependency);
            }

            foreach (var request in _ormDatabaseProviderStrategy.GetEnvironmentVariableRequests())
            {
                ExecutionContext.EventDispatcher.Publish(request);
            }
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();
            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest
                .Create(
                    moduleId: null,
                    statement: @$"TypeOrmModule.forRootAsync({{
      imports: [ConfigModule],
      useFactory: {ClassName},
      inject: [ConfigService]
    }})")
                .AddImport("ConfigModule", "@nestjs/config")
                .AddImport("ConfigService", "@nestjs/config")
                .AddImport("TypeOrmModule", "@nestjs/typeorm")
                .AddDependency(TemplateDependency.OnTemplate(this)));

            var repositories = Model.Select(x => GetTemplate<RepositoryTemplate>(RepositoryTemplate.TemplateId, x, new TemplateDiscoveryOptions() { TrackDependency = false })).ToList();

            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest
                .Create(
                    moduleId: null,
                    statement: $"TypeOrmModule.forFeature([{string.Join(", ", repositories.Select(x => x.ClassName))}])")
                .AddImport("TypeOrmModule", "@nestjs/typeorm")
                .AddDependencies(repositories.Select(TemplateDependency.OnTemplate).ToArray()));
        }

        private IEnumerable<string> GetDatabaseProviderOptions()
        {
            return _ormDatabaseProviderStrategy.GetConfigurationOptions();
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"typeOrmConfigFactory",
                fileName: $"orm.config"
            );
        }

    }
}