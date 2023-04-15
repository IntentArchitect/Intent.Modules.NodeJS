using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Authentication.Templates.Auth.AuthController;
using Intent.Modules.NestJS.Authentication.Templates.Auth.AuthModule;
using Intent.Modules.NestJS.Authentication.Templates.Auth.AuthService;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.JwtAuthGuard;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.LocalAuthGuard;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.PublicDecorator;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.JwtStrategy;
using Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.LocalStrategy;
using Intent.Modules.NestJS.Authentication.Templates.Users.UserContext;
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
        public static string GetAuthControllerName(this IIntentTemplate template)
        {
            return template.GetTypeName(AuthControllerTemplate.TemplateId);
        }
        public static string GetAuthModuleName(this IIntentTemplate template)
        {
            return template.GetTypeName(AuthModuleTemplate.TemplateId);
        }

        public static string GetAuthServiceName(this IIntentTemplate template)
        {
            return template.GetTypeName(AuthServiceTemplate.TemplateId);
        }

        public static string GetJwtAuthGuardName(this IIntentTemplate template)
        {
            return template.GetTypeName(JwtAuthGuardTemplate.TemplateId);
        }

        public static string GetJwtStrategyName(this IIntentTemplate template)
        {
            return template.GetTypeName(JwtStrategyTemplate.TemplateId);
        }

        public static string GetLocalAuthGuardName(this IIntentTemplate template)
        {
            return template.GetTypeName(LocalAuthGuardTemplate.TemplateId);
        }

        public static string GetLocalStrategyName(this IIntentTemplate template)
        {
            return template.GetTypeName(LocalStrategyTemplate.TemplateId);
        }

        public static string GetPublicDecoratorName(this IIntentTemplate template)
        {
            return template.GetTypeName(PublicDecoratorTemplate.TemplateId);
        }

        public static string GetUserContextName(this IIntentTemplate template)
        {
            return template.GetTypeName(UserContextTemplate.TemplateId);
        }

        public static string GetUsersModuleName(this IIntentTemplate template)
        {
            return template.GetTypeName(UsersModuleTemplate.TemplateId);
        }

        public static string GetUsersServiceName(this IIntentTemplate template)
        {
            return template.GetTypeName(UsersServiceTemplate.TemplateId);
        }

    }
}