using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Message;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Middyfy;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Schema;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates
{
    public static class TemplateExtensions
    {
        public static string GetControllerName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel
        {
            return template.GetTypeName(ControllerTemplate.TemplateId, template.Model);
        }

        public static string GetControllerName(this IntentTemplateBase template, Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel model)
        {
            return template.GetTypeName(ControllerTemplate.TemplateId, model);
        }

        public static string GetHandlerName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel
        {
            return template.GetTypeName(HandlerTemplate.TemplateId, template.Model);
        }

        public static string GetHandlerName(this IntentTemplateBase template, Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel model)
        {
            return template.GetTypeName(HandlerTemplate.TemplateId, model);
        }

        public static string GetMessageName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Api.MessageModel
        {
            return template.GetTypeName(MessageTemplate.TemplateId, template.Model);
        }

        public static string GetMessageName(this IntentTemplateBase template, Intent.Modelers.AWS.Api.MessageModel model)
        {
            return template.GetTypeName(MessageTemplate.TemplateId, model);
        }

        public static string GetMiddyfyName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(MiddyfyTemplate.TemplateId);
        }

        public static string GetSchemaName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Api.MessageModel
        {
            return template.GetTypeName(SchemaTemplate.TemplateId, template.Model);
        }

        public static string GetSchemaName(this IntentTemplateBase template, Intent.Modelers.AWS.Api.MessageModel model)
        {
            return template.GetTypeName(SchemaTemplate.TemplateId, model);
        }

    }
}