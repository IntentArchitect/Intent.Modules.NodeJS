using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Controllers.Templates.Controller;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Decorators
{
    [Description(ControllerAuthenticationDecorator.DecoratorId)]
    public class ControllerAuthenticationDecoratorRegistration : DecoratorRegistration<ControllerTemplate, ControllerDecorator>
    {
        public override ControllerDecorator CreateDecoratorInstance(ControllerTemplate template, IApplication application)
        {
            return new ControllerAuthenticationDecorator(template, application);
        }

        public override string DecoratorId => ControllerAuthenticationDecorator.DecoratorId;
    }
}