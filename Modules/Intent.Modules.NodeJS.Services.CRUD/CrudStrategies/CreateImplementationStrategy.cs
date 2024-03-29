using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Service;
using Intent.Modules.NodeJS.Services.CRUD.Decorators;
using Intent.Modules.TypeORM.Entities;
using Intent.Modules.TypeORM.Entities.Templates.Repository;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;
using ParameterModel = Intent.Modelers.Services.Api.ParameterModel;

namespace Intent.Modules.NodeJS.Services.CRUD.CrudStrategies
{
    class CreateImplementationStrategy : ICrudImplementationStrategy
    {
        private readonly ServiceTemplate _template;
        private readonly IApplication _application;
        private string _repository;

        public CreateImplementationStrategy(ServiceTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public bool IsMatch(ClassModel targetEntity, OperationModel operation)
        {
            var matches = new[]
            {
                $"add",
                $"add{targetEntity.Name.ToLower()}",
                $"addnew",
                $"addnew{targetEntity.Name.ToLower()}",
                $"create",
                $"create{targetEntity.Name.ToLower()}",
                $"createnew",
                $"createnew{targetEntity.Name.ToLower()}",
            }.Contains(operation.Name.ToLower());

            if (!matches)
            {
                return false;
            }

            if (!_template.TryGetTypeName(RepositoryTemplate.TemplateId, targetEntity, out _repository))
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
            var entityName = _template.TryGetTypeName(EntityTemplate.TemplateId, targetEntity);
            var impl = $@"const new{targetEntity.Name} = {{
      {GetPropertyAssignments(targetEntity, operation.Parameters.Single())}
    }} as {entityName ?? targetEntity.Name};

    await this.{_repository.ToCamelCase()}.insert(new{targetEntity.Name});";

            if (operation.TypeReference.Element != null)
            {
                impl += $@"
    return new{targetEntity.Name}.id;";
            }

            return impl;
        }

        private string GetPropertyAssignments(ClassModel domainModel, ParameterModel parameter)
        {
            var sb = new StringBuilder();
            var dto = new DTOModel((IElement) parameter.TypeReference.Element);
            foreach (var property in dto.Fields)
            {
                var attribute = domainModel.Attributes.FirstOrDefault(p => p.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                if (attribute == null)
                {
                    sb.AppendLine($"      // WARNING: No matching field found for {property.Name}");
                    continue;
                }
                if (attribute.Type.Element.Id != property.TypeReference.Element.Id)
                {
                    sb.AppendLine($"      // WARNING: No matching type for Domain: {attribute.Name} and DTO: {property.Name}");
                    continue;
                }
                sb.AppendLine($"      {attribute.Name.ToCamelCase()}: {parameter.Name}.{property.Name.ToCamelCase()},");
            }

            return sb.ToString().Trim();
        }
    }
}