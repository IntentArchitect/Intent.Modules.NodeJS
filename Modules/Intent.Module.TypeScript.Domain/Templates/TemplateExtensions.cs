using System.Collections.Generic;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Module.TypeScript.Domain.Templates.Enum;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Module.TypeScript.Domain.Templates
{
    public static class TemplateExtensions
    {
        public static string GetEntityName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Domain.Api.ClassModel
        {
            return template.GetTypeName(EntityTemplate.TemplateId, template.Model);
        }

        public static string GetEntityName(this IIntentTemplate template, Intent.Modelers.Domain.Api.ClassModel model)
        {
            return template.GetTypeName(EntityTemplate.TemplateId, model);
        }

        public static string GetEnumName<T>(this IIntentTemplate<T> template) where T : Intent.Modules.Common.Types.Api.EnumModel
        {
            return template.GetTypeName(EnumTemplate.TemplateId, template.Model);
        }

        public static string GetEnumName(this IIntentTemplate template, Intent.Modules.Common.Types.Api.EnumModel model)
        {
            return template.GetTypeName(EnumTemplate.TemplateId, model);
        }

    }
}