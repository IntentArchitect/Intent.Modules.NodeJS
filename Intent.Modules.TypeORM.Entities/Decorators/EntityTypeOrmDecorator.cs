using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.TypeORM.Entities.DatabaseProviders;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class EntityTypeOrmDecorator : EntityDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.TypeORM.EntityTypeOrmDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly EntityTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        private readonly IOrmDatabaseProviderStrategy _ormDatabaseProviderStrategy;

        [IntentManaged(Mode.Merge)]
        public EntityTypeOrmDecorator(EntityTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
            _template.AddDependency(new NpmPackageDependency("typeorm", "^0.2.32"));
            _ormDatabaseProviderStrategy = IOrmDatabaseProviderStrategy.Resolve(_template.OutputTarget);
        }

        public override string GetBeforeFields()
        {
            if (_template.Model.Attributes.Any(x => x.HasPrimaryKey()))
            {
                return base.GetBeforeFields();
            }
            return $@"
  @{_template.ImportType("ObjectIdColumn", "typeorm")}()
  @{_template.ImportType("PrimaryGeneratedColumn", "typeorm")}('uuid')
  id?: string;
";
        }

        public override string GetAfterFields()
        {
            var nullableTrueOption = new[] { "nullable: true" };

            var (stringColumnType, stringColumnOptions) = _ormDatabaseProviderStrategy.TryGetColumnType("string", out var stringColumnTypeOutput)
                ? ($"'{stringColumnTypeOutput.Type}', ", $"{string.Join(", ", nullableTrueOption.Concat(stringColumnTypeOutput.AdditionalOptions))}")
                : (string.Empty, string.Join(", ", nullableTrueOption));

            var (dateColumnType, dateColumnOptions) = _ormDatabaseProviderStrategy.TryGetColumnType("date", out var dateColumnTypeOutput)
                ? ($"'{dateColumnTypeOutput.Type}', ", $"{string.Join(", ", nullableTrueOption.Concat(dateColumnTypeOutput.AdditionalOptions))}")
                : (string.Empty, string.Join(", ", nullableTrueOption));

            return $@"
  @{_template.ImportType("Column", "typeorm")}({stringColumnType}{{ {stringColumnOptions} }})
  createdBy?: string;

  @{_template.ImportType("Column", "typeorm")}({dateColumnType}{{ {dateColumnOptions} }})
  createdDate?: Date;

  @{_template.ImportType("Column", "typeorm")}({stringColumnType}{{ {stringColumnOptions} }})
  lastModifiedBy?: string;

  @{_template.ImportType("Column", "typeorm")}({dateColumnType}{{ {dateColumnOptions} }})
  lastModifiedDate?: Date;";
        }

        public override IEnumerable<string> GetClassDecorators()
        {
            return new[] { $"@{_template.ImportType("Entity", "typeorm")}('{_template.ClassName.ToSnakeCase()}')" };
        }

        public override IEnumerable<string> GetFieldDecorators(AttributeModel attribute)
        {
            if (attribute.HasPrimaryKey())
            {
                return new[]
                {
                    $"@{_template.ImportType("ObjectIdColumn", "typeorm")}()",
                    $"@{_template.ImportType("PrimaryGeneratedColumn", "typeorm")}({(attribute.TypeReference.Element.Name == "guid" ? "'uuid'" : "")})"
                };
            }

            var parameters = new List<string>();
            var settings = new List<string>();

            if (attribute.TypeReference.IsNullable)
            {
                settings.Add("nullable: true");
            }

            var maxLength = attribute.GetTextConstraints()?.MaxLength();
            if (maxLength.HasValue)
            {
                settings.Add($"length: {maxLength.Value:D}");
            }


            if (_ormDatabaseProviderStrategy.TryGetColumnType(attribute, out var columnType))
            {
                settings.AddRange(columnType.AdditionalOptions);
                parameters.Add($"'{columnType.Type}'");
            }

            if (settings.Count > 0)
            {
                parameters.Add($"{{ {string.Join(", ", settings)} }}");
            }

            return new[] { $"@{_template.ImportType("Column", "typeorm")}({string.Join(", ", parameters)})" };
        }

        public override IEnumerable<string> GetFieldDecorators(AssociationEndModel thatEnd)
        {
            if (!thatEnd.IsNavigable)
            {
                throw new InvalidOperationException("Cannot call this method if associationEnd is not navigable.");
            }

            var statements = new List<string>();
            var sourceEnd = thatEnd.OtherEnd();
            if (!sourceEnd.IsCollection && !thatEnd.IsCollection) // one-to-one
            {
                statements.Add($"@{_template.ImportType("OneToOne", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name.ToCamelCase()} => {thatEnd.Name.ToCamelCase()}.{sourceEnd.Name.ToCamelCase()}" : "")})");
                if (thatEnd.IsTargetEnd())
                {
                    statements.Add($"@{_template.ImportType("JoinColumn", "typeorm")}()");
                }
            }
            else if (!sourceEnd.IsCollection && thatEnd.IsCollection) // one-to-many
            {
                statements.Add($"@{_template.ImportType("OneToMany", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name.ToCamelCase()} => {thatEnd.Name.ToCamelCase()}.{sourceEnd.Name.ToCamelCase()}" : "")})");
            }
            else if (sourceEnd.IsCollection && !thatEnd.IsCollection) // many-to-one
            {
                statements.Add($"@{_template.ImportType("ManyToOne", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name.ToCamelCase()} => {thatEnd.Name.ToCamelCase()}.{sourceEnd.Name.ToCamelCase()}" : "")})");
            }
            else if (sourceEnd.IsCollection && thatEnd.IsCollection) // many-to-many
            {
                statements.Add($"@{_template.ImportType("ManyToMany", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(sourceEnd.IsNavigable ? $", {thatEnd.Name.ToCamelCase()} => {thatEnd.Name.ToCamelCase()}.{sourceEnd.Name.ToCamelCase()}" : "")})");
                if (thatEnd.IsTargetEnd())
                {
                    statements.Add($"@{_template.ImportType("JoinTable", "typeorm")}()");
                }
            }

            return statements;
        }
    }
}