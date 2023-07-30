using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NodeJS.Services.CRUD.Decorators
{
    [Description(ServiceCrudImplementationDecorator.DecoratorId)]
    public class ServiceCrudImplementationDecoratorRegistration : DecoratorRegistration<ServiceTemplate, ServiceDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public override ServiceDecorator CreateDecoratorInstance(ServiceTemplate template, IApplication application)
        {
            return new ServiceCrudImplementationDecorator(template, application);
        }

        public override string DecoratorId => ServiceCrudImplementationDecorator.DecoratorId;
    }
}