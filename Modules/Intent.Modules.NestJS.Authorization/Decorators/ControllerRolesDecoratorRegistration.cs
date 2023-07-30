using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Controllers.Templates.Controller;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Decorators
{
    [Description(ControllerRolesDecorator.DecoratorId)]
    public class ControllerRolesDecoratorRegistration : DecoratorRegistration<ControllerTemplate, ControllerDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public override ControllerDecorator CreateDecoratorInstance(ControllerTemplate template, IApplication application)
        {
            return new ControllerRolesDecorator(template, application);
        }

        public override string DecoratorId => ControllerRolesDecorator.DecoratorId;
    }
}