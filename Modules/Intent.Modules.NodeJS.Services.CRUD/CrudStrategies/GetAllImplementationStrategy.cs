using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class GetAllImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private string _repository;
        private DTOModel _dtoToReturn;

        public GetAllImplementationStrategy(ServiceTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            if (operation.TypeReference.Element == null || !operation.TypeReference.IsCollection)
            {
                return false;
            }

            var matches = new[]
            {
                $"get",
                $"get{targetEntity.Name.ToPluralName().ToLower()}",
                $"getall",
                $"getall{targetEntity.Name.ToPluralName().ToLower()}",
                $"find",
                $"find{targetEntity.Name.ToPluralName().ToLower()}",
                $"findall",
                $"findall{targetEntity.Name.ToPluralName().ToLower()}",
                $"lookup",
                $"lookup{targetEntity.Name.ToPluralName().ToLower()}",
                $"lookupall",
                $"lookupall{targetEntity.Name.ToPluralName().ToLower()}",
            }.Contains(operation.Name.ToLower());

            if (!matches)
            {
                return false;
            }

            _dtoToReturn = _application.MetadataManager.Services(_application).GetDTOModels().SingleOrDefault(x => x.Id == operation.TypeReference.Element.Id && x.IsMapped && x.Mapping.ElementId == targetEntity.Id);
            if (_dtoToReturn == null)
            {
                return false;
            }

            if (!_template.TryGetTypeName(RepositoryTemplate.TemplateId, targetEntity, out _repository))
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
            return $@"const {targetEntity.Name.ToCamelCase().ToPluralName()} = await this.{_repository.ToCamelCase()}.find({{
      relations: {dto}.requiredRelations,
    }});
    return {targetEntity.Name.ToCamelCase().ToPluralName()}.map((x) => {dto}.from{targetEntity.Name.ToPascalCase()}(x));";
        }
    }
}