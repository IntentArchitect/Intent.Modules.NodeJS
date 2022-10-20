using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Templates;
using Intent.Modules.Metadata.RDBMS.Settings;
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
            _template.AddDependency(NpmPackageDependencies.TypeOrm);
            _ormDatabaseProviderStrategy = IOrmDatabaseProviderStrategy.Resolve(_template.OutputTarget);
        }

        public override string GetBeforeFields()
        {
            if (_template.Model.ParentClass != null ||
                _template.Model.Attributes.Any(x => x.HasPrimaryKey()))
            {
                return base.GetBeforeFields();
            }

            var (typeName, typeScriptType, strategy) = _application.Settings.GetDatabaseSettings().KeyType().AsEnum() switch
            {
                DatabaseSettings.KeyTypeOptionsEnum.Guid => ("guid", "string", "'uuid'"),
                DatabaseSettings.KeyTypeOptionsEnum.Long => ("long", "number", string.Empty),
                DatabaseSettings.KeyTypeOptionsEnum.Int => ("int", "number", string.Empty),
                _ => throw new ArgumentOutOfRangeException()
            };

            var columnType = _ormDatabaseProviderStrategy.TryGetColumnType(typeName, out var stringColumnTypeOutput)
                ? stringColumnTypeOutput.Type
                : typeScriptType;

            return $@"
  @{_template.ImportType("Column", "typeorm")}('{columnType}')
  @{_template.ImportType("ObjectIdColumn", "typeorm")}()
  @{_template.ImportType("PrimaryGeneratedColumn", "typeorm")}({strategy})
  id?: {typeScriptType};
";
        }

        public override string GetAfterFields()
        {
            if (_template.Model.ParentClass != null)
            {
                return string.Empty;
            }

            var nullableTrueOption = new[] { "nullable: true" };

            var (stringColumnType, stringColumnOptions) = _ormDatabaseProviderStrategy.TryGetColumnType("string", out var stringColumnTypeOutput)
                ? ($"'{stringColumnTypeOutput.Type}', ", $"{string.Join(", ", nullableTrueOption.Concat(stringColumnTypeOutput.AdditionalOptions))}")
                : (string.Empty, string.Join(", ", nullableTrueOption));

            var (dateColumnType, dateColumnOptions) = _ormDatabaseProviderStrategy.TryGetColumnType("datetime", out var dateColumnTypeOutput)
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
                if (_template.Model.GetExplicitPrimaryKey().Count > 1 ||
                    attribute.TypeReference.Element.Name == "string")
                {
                    yield return $"@{_template.ImportType("PrimaryColumn", "typeorm")}()";
                }
                else
                {
                    // ObjectIdColumn should only be used when we have MongoDD database:
                    // Special type of column that is available only for MongoDB database. Marks your entity's column to be an object id.
                    //yield return $"@{_template.ImportType("ObjectIdColumn", "typeorm")}()";

                    yield return $"@{_template.ImportType("PrimaryGeneratedColumn", "typeorm")}({(attribute.TypeReference.Element.Name == "guid" ? "'uuid'" : "")})";
                }
                yield break;
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

            yield return $"@{_template.ImportType("Column", "typeorm")}({string.Join(", ", parameters)})";
        }

        public override IEnumerable<string> GetFieldDecorators(AssociationEndModel thatEnd)
        {
            static bool IsCompositeOwner(AssociationEndModel associationEnd)
            {
                return associationEnd.Equals(associationEnd.Association.TargetEnd) &&
                       associationEnd.Association.SourceEnd.Multiplicity == Multiplicity.One;
            }

            var thisEnd = thatEnd.OtherEnd();

            var options = new List<string>();

            if (IsCompositeOwner(thatEnd))
            {
                options.Add("cascade: true");
                options.Add("eager: true");
            }
            else
            {
                options.Add("cascade: ['insert', 'update']");
            }

            if (!thatEnd.IsNullable && !thatEnd.IsCollection)
            {
                options.Add("nullable: false");
            }

            if (!IsCompositeOwner(thatEnd) &&
                IsCompositeOwner(thisEnd))
            {
                options.Add("onDelete: 'CASCADE', orphanedRowAction: 'delete'");
            }

            var statements = new List<string>();
            if (!thisEnd.IsCollection && !thatEnd.IsCollection) // one-to-one
            {
                statements.Add($"@{_template.ImportType("OneToOne", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(thisEnd.IsNavigable ? $", {thatEnd.Name.ToCamelCase()} => {thatEnd.Name.ToCamelCase()}.{thisEnd.Name.ToCamelCase()}" : "")}, {{ {string.Join(", ", options)} }})");
                if (thatEnd.IsTargetEnd())
                {
                    statements.Add($"@{_template.ImportType("JoinColumn", "typeorm")}()");
                }
            }
            else if (!thisEnd.IsCollection && thatEnd.IsCollection) // one-to-many
            {
                // TypeOrm does not support support unidirectional one-to-many and many-to-one relationships when using decorators:
                // https://github.com/typeorm/typeorm/issues/3233
                statements.Add(@$"@{_template.ImportType("OneToMany", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}, ({thatEnd.Name.ToCamelCase()}) => {thatEnd.Name.ToCamelCase()}.{thisEnd.Name.ToCamelCase()}, {{ {string.Join(", ", options)} }})");
            }
            else if (thisEnd.IsCollection && !thatEnd.IsCollection) // many-to-one
            {
                statements.Add($"@{_template.ImportType("ManyToOne", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(thisEnd.IsNavigable ? $", ({thatEnd.Name.ToCamelCase()}) => {thatEnd.Name.ToCamelCase()}.{thisEnd.Name.ToCamelCase()}" : "")}, {{ {string.Join(", ", options)} }})");
            }
            else if (thisEnd.IsCollection && thatEnd.IsCollection) // many-to-many
            {
                statements.Add($"@{_template.ImportType("ManyToMany", "typeorm")}(() => {_template.GetTypeName(EntityTemplate.TemplateId, thatEnd.Element)}{(thisEnd.IsNavigable ? $", ({thatEnd.Name.ToCamelCase()}) => {thatEnd.Name.ToCamelCase()}.{thisEnd.Name.ToCamelCase()}" : "")}, {{ {string.Join(", ", options)} }})");
                if (thatEnd.IsTargetEnd())
                {
                    statements.Add($"@{_template.ImportType("JoinTable", "typeorm")}()");
                }
            }

            return statements;
        }

        public override bool RequiresAssociationFieldFor(AssociationEndModel thatEnd)
        {
            // TypeOrm does not support support unidirectional one-to-many relationships when using decorator approach:
            // https://github.com/typeorm/typeorm/issues/3233
            return !thatEnd.IsCollection && thatEnd.OtherEnd().IsCollection;
        }
    }
}