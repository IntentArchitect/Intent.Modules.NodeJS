using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Authorization.Templates.RoleEnum;
using Intent.Modules.NestJS.Authorization.Templates.RolesDecorator;
using Intent.Modules.NestJS.Authorization.Templates.RolesGuard;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Templates
{
    public static class TemplateExtensions
    {
        public static string GetRoleEnumName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(RoleEnumTemplate.TemplateId);
        }

        public static string GetRolesDecoratorName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(RolesDecoratorTemplate.TemplateId);
        }

        public static string GetRolesGuardName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(RolesGuardTemplate.TemplateId);
        }

    }
}