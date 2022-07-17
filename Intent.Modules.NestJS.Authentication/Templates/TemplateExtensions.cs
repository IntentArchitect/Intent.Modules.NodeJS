using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Authentication.Templates.Auth.AuthModule;
using Intent.Modules.NestJS.Authentication.Templates.Auth.AuthService;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.JwtAuthGuard;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.LocalAuthGuard;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.PublicDecorator;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.JwtStrategy;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.LocalStrategy;
using Intent.Modules.NestJS.Authentication.Templates.Users.User;
using Intent.Modules.NestJS.Authentication.Templates.Users.UsersModule;
using Intent.Modules.NestJS.Authentication.Templates.Users.UsersService;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates
{
    public static class TemplateExtensions
    {
        public static string GetAuthModuleName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(AuthModuleTemplate.TemplateId);
        }

        public static string GetAuthServiceName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(AuthServiceTemplate.TemplateId);
        }

        public static string GetJwtAuthGuardName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(JwtAuthGuardTemplate.TemplateId);
        }

        public static string GetJwtStrategyName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(JwtStrategyTemplate.TemplateId);
        }

        public static string GetLocalAuthGuardName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(LocalAuthGuardTemplate.TemplateId);
        }

        public static string GetLocalStrategyName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(LocalStrategyTemplate.TemplateId);
        }

        public static string GetPublicDecoratorName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(PublicDecoratorTemplate.TemplateId);
        }

        public static string GetUserName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(UserTemplate.TemplateId);
        }

        public static string GetUsersModuleName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(UsersModuleTemplate.TemplateId);
        }

        public static string GetUsersServiceName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(UsersServiceTemplate.TemplateId);
        }

    }
}