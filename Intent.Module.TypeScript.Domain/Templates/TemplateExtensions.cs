using System.Collections.Generic;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Module.TypeScript.Domain.Templates
{
    public static class TemplateExtensions
    {
        public static string GetEntityName<T>(this IntentTemplateBase<T> template) where T : Intent.Modelers.Domain.Api.ClassModel
        {
            return template.GetTypeName(EntityTemplate.TemplateId, template.Model);
        }

        public static string GetEntityName(this IntentTemplateBase template, Intent.Modelers.Domain.Api.ClassModel model)
        {
            return template.GetTypeName(EntityTemplate.TemplateId, model);
        }

    }
}