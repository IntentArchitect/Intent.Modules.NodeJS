using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Core.Templates.AppModule;
using Intent.Modules.NestJS.Core.Templates.Main;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates
{
    public static class TemplateExtensions
    {
        public static string GetAppModuleName(this IIntentTemplate template)
        {
            return template.GetTypeName(AppModuleTemplate.TemplateId);
        }

        public static string GetMainName(this IIntentTemplate template)
        {
            return template.GetTypeName(MainTemplate.TemplateId);
        }

    }
}