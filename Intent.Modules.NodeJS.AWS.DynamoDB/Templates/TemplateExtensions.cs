using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.DocumentClientProvider;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItem;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemMapAttribute;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepositories;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepository;
using Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepositoryBase;
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

        public static string GetTableItemName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel
        {
            return template.GetTypeName(TableItemTemplate.TemplateId, template.Model);
        }

        public static string GetTableItemName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel model)
        {
            return template.GetTypeName(TableItemTemplate.TemplateId, model);
        }

        public static string GetTableItemMapAttributeName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.MapAttributeModel
        {
            return template.GetTypeName(TableItemMapAttributeTemplate.TemplateId, template.Model);
        }

        public static string GetTableItemMapAttributeName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.MapAttributeModel model)
        {
            return template.GetTypeName(TableItemMapAttributeTemplate.TemplateId, model);
        }

        public static string GetTableItemRepositoriesName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel
        {
            return template.GetTypeName(TableItemRepositoriesTemplate.TemplateId, template.Model);
        }

        public static string GetTableItemRepositoriesName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel model)
        {
            return template.GetTypeName(TableItemRepositoriesTemplate.TemplateId, model);
        }

        public static string GetTableItemRepositoryName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel
        {
            return template.GetTypeName(TableItemRepositoryTemplate.TemplateId, template.Model);
        }

        public static string GetTableItemRepositoryName(this IntentTemplateBase template, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel model)
        {
            return template.GetTypeName(TableItemRepositoryTemplate.TemplateId, model);
        }

        public static string GetTableItemRepositoryBaseName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(TableItemRepositoryBaseTemplate.TemplateId);
        }

    }
}