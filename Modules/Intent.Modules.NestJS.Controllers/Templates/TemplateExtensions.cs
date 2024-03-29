using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Controller;
using Intent.Modules.NestJS.Controllers.Templates.ControllerBase;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Controllers.Templates.JsonResponse;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates
{
    public static class TemplateExtensions
    {
        public static string GetControllerName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Services.Api.ServiceModel
        {
            return template.GetTypeName(ControllerTemplate.TemplateId, template.Model);
        }

        public static string GetControllerName(this IIntentTemplate template, Intent.Modelers.Services.Api.ServiceModel model)
        {
            return template.GetTypeName(ControllerTemplate.TemplateId, model);
        }

        public static string GetControllerBaseName(this IIntentTemplate template)
        {
            return template.GetTypeName(ControllerBaseTemplate.TemplateId);
        }

        public static string GetDtoModelName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Services.Api.DTOModel
        {
            return template.GetTypeName(DtoModelTemplate.TemplateId, template.Model);
        }

        public static string GetDtoModelName(this IIntentTemplate template, Intent.Modelers.Services.Api.DTOModel model)
        {
            return template.GetTypeName(DtoModelTemplate.TemplateId, model);
        }

        public static string GetJsonResponseName(this IIntentTemplate template)
        {
            return template.GetTypeName(JsonResponseTemplate.TemplateId);
        }

        public static string GetServiceName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Services.Api.ServiceModel
        {
            return template.GetTypeName(ServiceTemplate.TemplateId, template.Model);
        }

        public static string GetServiceName(this IIntentTemplate template, Intent.Modelers.Services.Api.ServiceModel model)
        {
            return template.GetTypeName(ServiceTemplate.TemplateId, model);
        }

    }
}