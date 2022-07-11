using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities.Templates.RepositoryTemplate;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;
using ParameterModel = Intent.Modelers.Services.Api.ParameterModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class UpdateImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private readonly IMetadataManager _metadataManager;
        private string _repository;
        private ParameterModel _idParam;

        public UpdateImplementationStrategy(ServiceTemplate template, IApplication application, IMetadataManager metadataManager)
        {
            _template = template;
            _application = application;
            _metadataManager = metadataManager;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            var matches = new[]
            {
                $"update",
                $"update{targetEntity.Name.ToLower()}",
                $"updatedetails",
                $"update{targetEntity.Name.ToLower()}details",
                $"edit",
                $"edit{targetEntity.Name.ToLower()}",
                $"editdetails",
                $"edit{targetEntity.Name.ToLower()}details",
            }.Contains(operation.Name.ToLower());

            if (!matches)
            {
                return false;
            }

            _idParam = operation.Parameters.FirstOrDefault(p =>
                string.Equals(p.Name, "id", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(p.Name, $"{targetEntity.Name}Id", StringComparison.InvariantCultureIgnoreCase));
            if (_idParam == null)
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
            return $@"var existing{targetEntity.Name} = await this.{_repository.ToCamelCase()}.findOne({_idParam.Name.ToCamelCase()});
    {GetPropertyAssignments(targetEntity, operation.Parameters.Single(x => !x.Equals(_idParam)))}

    await this.{_repository.ToCamelCase()}.save(existing{targetEntity.Name});";
        }

        private string GetPropertyAssignments(ClassModel targetEntity, ParameterModel dtoParam)
        {
            var sb = new StringBuilder();
            var dto = new DTOModel((IElement) dtoParam.TypeReference.Element);
            foreach (var property in dto.Fields)
            {
                var attribute = targetEntity.Attributes.FirstOrDefault(p => p.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                if (attribute == null)
                {
                    sb.AppendLine($"    // WARNING: No matching field found for {property.Name}");
                    continue;
                }
                if (attribute.Type.Element.Id != property.TypeReference.Element.Id)
                {
                    sb.AppendLine($"    // WARNING: No matching type for Domain: {attribute.Name} and DTO: {property.Name}");
                    continue;
                }
                sb.AppendLine($"    existing{targetEntity.Name.ToPascalCase()}.{attribute.Name.ToCamelCase()} = {dtoParam.Name.ToCamelCase()}.{property.Name.ToCamelCase()};");
            }

            return sb.ToString().Trim();
        }
    }
}