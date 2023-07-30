using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Authentication.Templates.Users.UserContext;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Decorators
{
    [Description(UserContextRolesDecorator.DecoratorId)]
    public class UserContextRolesDecoratorRegistration : DecoratorRegistration<UserContextTemplate, UserContextDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public override UserContextDecorator CreateDecoratorInstance(UserContextTemplate template, IApplication application)
        {
            return new UserContextRolesDecorator(template, application);
        }

        public override string DecoratorId => UserContextRolesDecorator.DecoratorId;
    }
}