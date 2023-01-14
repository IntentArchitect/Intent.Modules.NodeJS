using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.DocumentClientProvider;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.Entity;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepositories;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepository;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepositoryBase;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates
{
    public static class TemplateExtensions
    {
        public static string GetDocumentClientProviderName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(DocumentClientProviderTemplate.TemplateId);
        }

        public static string GetEntityName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.EntityModel
        {
            return template.GetTypeName(EntityTemplate.TemplateId, template.Model);
        }

        public static string GetEntityName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.EntityModel model)
        {
            return template.GetTypeName(EntityTemplate.TemplateId, model);
        }

        public static string GetEntityRepositoriesName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel
        {
            return template.GetTypeName(EntityRepositoriesTemplate.TemplateId, template.Model);
        }

        public static string GetEntityRepositoriesName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel model)
        {
            return template.GetTypeName(EntityRepositoriesTemplate.TemplateId, model);
        }

        public static string GetEntityRepositoryName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.EntityModel
        {
            return template.GetTypeName(EntityRepositoryTemplate.TemplateId, template.Model);
        }

        public static string GetEntityRepositoryName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.EntityModel model)
        {
            return template.GetTypeName(EntityRepositoryTemplate.TemplateId, model);
        }

        public static string GetEntityRepositoryBaseName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(EntityRepositoryBaseTemplate.TemplateId);
        }

    }
}