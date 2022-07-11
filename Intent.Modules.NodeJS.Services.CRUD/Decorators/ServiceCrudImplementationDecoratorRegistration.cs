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
        private readonly IMetadataManager _metadataManager;

        public ServiceCrudImplementationDecoratorRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override ServiceDecorator CreateDecoratorInstance(ServiceTemplate template, IApplication application)
        {
            return new ServiceCrudImplementationDecorator(template, application, _metadataManager);
        }

        public override string DecoratorId => ServiceCrudImplementationDecorator.DecoratorId;
    }
}