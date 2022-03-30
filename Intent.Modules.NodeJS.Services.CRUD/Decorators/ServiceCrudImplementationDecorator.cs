using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.ServiceTemplate;
using Intent.Modules.NodeJS.Services.CRUD.CrudStrategies;
using Intent.RoslynWeaver.Attributes;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NodeJS.Services.CRUD.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class ServiceCrudImplementationDecorator : ServiceDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.Services.CRUD.ServiceCrudImplementationDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly ServiceTemplate _template;
        private readonly IMetadataManager _metadataManager;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;
        private readonly ICrudImplementationStrategy[] _strategies;
        private readonly ClassModel _targetEntity;

        [IntentManaged(Mode.Ignore)]
        public ServiceCrudImplementationDecorator(ServiceTemplate template, IApplication application, IMetadataManager metadataManager)
        {
            _template = template;
            _application = application;
            _metadataManager = metadataManager;

            _strategies = new ICrudImplementationStrategy[]
            {
                new GetAllImplementationStrategy(_template, application, _metadataManager),
                new GetByIdImplementationStrategy(_template, application, _metadataManager),
                new CreateImplementationStrategy(_template, application, _metadataManager),
                new UpdateImplementationStrategy(_template, application, _metadataManager),
                new DeleteImplementationStrategy(_template, application, _metadataManager),
            };
            _targetEntity = GetDomainForService(_template.Model);
        }

        public override IEnumerable<string> GetConstructorParameters()
        {
            if (_targetEntity == null)
            {
                return base.GetConstructorParameters();
            }

            return _template.Model.Operations.SelectMany(op =>
                _strategies.Where(x => x.IsMatch(_targetEntity, op)).SelectMany(x => x.GetRequiredServices())).Distinct();
        }

        public override string GetImplementation(OperationModel operation)
        {
            if (_targetEntity == null)
            {
                return string.Empty;
            }

            foreach (var strategy in _strategies)
            {
                if (strategy.IsMatch(_targetEntity, operation))
                {
                    return strategy.GetImplementation(_targetEntity, operation);
                }
            }

            return string.Empty;
        }

        private ClassModel GetDomainForService(ServiceModel service)
        {
            var serviceIdentifier = service.Name.RemoveSuffix("RestController", "Controller", "Service", "Manager").ToLower();
            var entities = _metadataManager.Domain(_template.OutputTarget.Application).GetClassModels();
            return entities.SingleOrDefault(e => e.Name.Equals(serviceIdentifier, StringComparison.InvariantCultureIgnoreCase) ||
                                                 e.Name.Pluralize().Equals(serviceIdentifier, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}