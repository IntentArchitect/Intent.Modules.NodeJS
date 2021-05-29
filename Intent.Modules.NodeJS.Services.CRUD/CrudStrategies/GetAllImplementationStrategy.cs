using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Controllers.Templates.ServiceTemplate;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities.Templates.RepositoryTemplate;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class GetAllImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private readonly IMetadataManager _metadataManager;
        private string _repository;
        private DTOModel _dtoToReturn;

        public GetAllImplementationStrategy(ServiceTemplate template, IApplication application, IMetadataManager metadataManager)
        {
            _template = template;
            _application = application;
            _metadataManager = metadataManager;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            if (operation.TypeReference.Element == null || !operation.TypeReference.IsCollection)
            {
                return false;
            }

            var matches = new[]
            {
                $"get{targetEntity.Name.ToPluralName().ToLower()}",
                $"getall{targetEntity.Name.ToPluralName().ToLower()}",
                $"find{targetEntity.Name.ToPluralName().ToLower()}",
                $"findall{targetEntity.Name.ToPluralName().ToLower()}",
                $"lookup{targetEntity.Name.ToPluralName().ToLower()}",
                $"lookupall{targetEntity.Name.ToPluralName().ToLower()}",
            }.Contains(operation.Name.ToLower());

            if (!matches)
            {
                return false;
            }

            _dtoToReturn = _metadataManager.Services(_application).GetDTOModels().SingleOrDefault(x => x.Id == operation.TypeReference.Element.Id && x.IsMapped && x.Mapping.ElementId == targetEntity.Id);
            if (_dtoToReturn == null)
            {
                return false;
            }

            _repository = _template.TryGetTypeName(RepositoryTemplate.TemplateId, targetEntity);
            if (_repository == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<string> GetRequiredServices()
        {
            return new[]
            {
                $"private {_repository.ToCamelCase()}: {_repository}"
            };
        }

        public string GetImplementation(ClassModel targetEntity, OperationModel operation)
        {
            return $@"var element = await this.{_repository.ToCamelCase()}.FindAllAsync(cancellationToken);
    return element.mapTo{_template.GetTypeName(DtoModelTemplate.TemplateId, _dtoToReturn)}List(_mapper);";
        }
    }
}