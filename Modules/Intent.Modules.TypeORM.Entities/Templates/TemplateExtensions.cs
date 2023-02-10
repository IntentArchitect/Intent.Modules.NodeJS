using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.TypeORM.Entities.Templates.BasicAuditingSubscriber;
using Intent.Modules.TypeORM.Entities.Templates.Datasource;
using Intent.Modules.TypeORM.Entities.Templates.OrmConfig;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using Intent.Modules.TypeORM.Entities.Templates.TypeOrmExDecorator;
using Intent.Modules.TypeORM.Entities.Templates.TypeOrmExModule;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates
{
    public static class TemplateExtensions
    {
        public static string GetBasicAuditingSubscriberName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(BasicAuditingSubscriberTemplate.TemplateId);
        }
        public static string GetDatasourceName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(DatasourceTemplate.TemplateId);
        }
        public static string GetOrmConfigName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(OrmConfigTemplate.TemplateId);
        }

        public static string GetRepositoryTemplateName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.Domain.Api.ClassModel
        {
            return template.GetTypeName(RepositoryTemplate.TemplateId, template.Model);
        }

        public static string GetRepositoryTemplateName(this IntentTemplateBase template, Intent.Modelers.Domain.Api.ClassModel model)
        {
            return template.GetTypeName(RepositoryTemplate.TemplateId, model);
        }

        public static string GetTypeOrmExDecoratorName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(TypeOrmExDecoratorTemplate.TemplateId);
        }

        public static string GetTypeOrmExModuleName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(TypeOrmExModuleTemplate.TemplateId);
        }

    }
}