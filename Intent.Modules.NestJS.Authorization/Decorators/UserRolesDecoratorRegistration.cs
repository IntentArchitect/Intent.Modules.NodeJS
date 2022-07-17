using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Authentication.Templates.Users.User;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Decorators
{
    [Description(UserRolesDecorator.DecoratorId)]
    public class UserRolesDecoratorRegistration : DecoratorRegistration<UserTemplate, UserDecorator>
    {
        public override UserDecorator CreateDecoratorInstance(UserTemplate template, IApplication application)
        {
            return new UserRolesDecorator(template, application);
        }

        public override string DecoratorId => UserRolesDecorator.DecoratorId;
    }
}