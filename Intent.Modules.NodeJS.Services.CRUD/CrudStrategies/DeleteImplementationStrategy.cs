using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;
using ParameterModel = Intent.Modelers.Services.Api.ParameterModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class DeleteImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private readonly IMetadataManager _metadataManager;
        private string _repository;
        private ParameterModel _idParam;

        public DeleteImplementationStrategy(ServiceTemplate template, IApplication application, IMetadataManager metadataManager)
        {
            _template = template;
            _application = application;
            _metadataManager = metadataManager;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            if (operation.Parameters.Count() != 1)
            {
                return false;
            }

            var matches = new[]
            {
                $"delete",
                $"delete{targetEntity.Name.ToLower()}",
                $"remove",
                $"remove{targetEntity.Name.ToLower()}",
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

            if (_template.TryGetTypeName(RepositoryTemplate.TemplateId, targetEntity, out _repository))
            {
                return false;
            }

            // Support for composite primary keys not implemented:
            if (targetEntity.GetPrimaryKeys().PrimaryKeys.Count > 1)
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
            return $@"const existing{targetEntity.Name} = await this.{_repository.ToCamelCase()}.findOneBy({{
      id: {_idParam.Name.ToCamelCase()},
    }});
    await this.{_repository.ToCamelCase()}.remove(existing{targetEntity.Name});";
        }
    }
}