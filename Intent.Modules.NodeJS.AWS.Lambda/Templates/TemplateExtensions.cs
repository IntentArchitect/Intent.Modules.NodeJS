using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Middyfy;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Payload;
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

        public static string GetMiddyfyName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(MiddyfyTemplate.TemplateId);
        }

        public static string GetPayloadName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Api.DTOModel
        {
            return template.GetTypeName(PayloadTemplate.TemplateId, template.Model);
        }

        public static string GetPayloadName(this IntentTemplateBase template, Intent.Modelers.AWS.Api.DTOModel model)
        {
            return template.GetTypeName(PayloadTemplate.TemplateId, model);
        }

        public static string GetSchemaName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.AWS.Api.DTOModel
        {
            return template.GetTypeName(SchemaTemplate.TemplateId, template.Model);
        }

        public static string GetSchemaName(this IntentTemplateBase template, Intent.Modelers.AWS.Api.DTOModel model)
        {
            return template.GetTypeName(SchemaTemplate.TemplateId, model);
        }

    }
}