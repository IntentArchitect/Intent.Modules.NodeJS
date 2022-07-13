using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;
using ParameterModel = Intent.Modelers.Services.Api.ParameterModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class GetByIdImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private readonly IMetadataManager _metadataManager;
        private string _repository;
        private DTOModel _dtoToReturn;
        private ParameterModel _idProperty;

        public GetByIdImplementationStrategy(ServiceTemplate template, IApplication application, IMetadataManager metadataManager)
        {
            _template = template;
            _application = application;
            _metadataManager = metadataManager;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            if (operation.TypeReference.Element == null)
            {
                return false;
            }

            if (operation.Parameters.Count() != 1)
            {
                return false;
            }

            var matches = new[]
            {
                $"get",
                $"get{targetEntity.Name.ToLower()}",
                $"getbyid",
                $"get{targetEntity.Name.ToLower()}byid",
                $"find",
                $"find{targetEntity.Name.ToLower()}",
                $"findbyid",
                $"find{targetEntity.Name.ToLower()}byid",
                $"lookup",
                $"lookup{targetEntity.Name.ToLower()}",
                $"lookupbyid",
                $"lookup{targetEntity.Name.ToLower()}byid",
            }.Contains(operation.Name.ToLower().RemoveSuffix("query"));

            if (!matches)
            {
                return false;
            }

            _dtoToReturn = _metadataManager.Services(_application).GetDTOModels().SingleOrDefault(x => x.Id == operation.TypeReference.Element.Id && x.IsMapped && x.Mapping.ElementId == targetEntity.Id);
            if (_dtoToReturn == null)
            {
                return false;
            }

            _idProperty = operation.Parameters.FirstOrDefault(p =>
                string.Equals(p.Name, "id", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(p.Name, $"{targetEntity.Name}Id", StringComparison.InvariantCultureIgnoreCase));
            if (_idProperty == null)
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
            var dto = _template.GetTypeName(DtoModelTemplate.TemplateId, _dtoToReturn);
            return $@"var {targetEntity.Name.ToCamelCase()} = await this.{_repository.ToCamelCase()}.findOne({_idProperty.Name.ToCamelCase()}, {{ relations: {dto}.requiredRelations }});
    return {dto}.from{targetEntity.Name.ToPascalCase()}({targetEntity.Name.ToCamelCase()});";
        }
    }
}