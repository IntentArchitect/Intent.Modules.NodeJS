using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Utils;
using GeneralizationModel = Intent.Modelers.Domain.Api.GeneralizationModel;
using OperationModel = Intent.Modelers.Domain.Api.OperationModel;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.DtoModel
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class DtoModelTemplate : TypeScriptTemplateBase<Intent.Modelers.Services.Api.DTOModel, DtoModelDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Controllers.DtoModel";

        private readonly ClassModel MappedModel;

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DtoModelTemplate(IOutputTarget outputTarget, Intent.Modelers.Services.Api.DTOModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
            AddTypeSource(EntityTemplate.TemplateId);

            if (Model.Fields.Any(IsDate))
            {
                AddDependency(NpmPackageDependencies.ClassTransformer);
            }

            if (Model.IsMapped)
            {
                MappedModel = Model.Mapping.Element.AsClassModel();
            }
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.RemoveSuffix("DTO", "Dto").ToKebabCase()}.dto",
                relativeLocation: this.GetFolderPath());
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            foreach (var decorator in GetDecorators())
            {
                decorator.BeforeTemplateExecution();
            }
        }

        private IEnumerable<string> GetFieldDecorators(DTOFieldModel field)
        {
            foreach (var decorator in GetDecorators().SelectMany(x => x.GetDecorators(field)))
            {
                yield return decorator;
            }

            var apiPropertyOptions = new List<string>();
            if (!string.IsNullOrWhiteSpace(field.Comment))
            {
                apiPropertyOptions.Add($"description: \"{field.Comment}\"");
            }
            yield return $"@{ImportType("ApiProperty", "@nestjs/swagger")}({(apiPropertyOptions.Any() ? $"{{ {string.Join(", ", apiPropertyOptions)} }}" : "")})";

            if (IsDate(field))
            {
                yield return $"@{ImportType("Type", "class-transformer")}(() => Date)";
            }
        }

        private string GetMappings()
        {
            var entity = new ClassModel((IElement)Model.Mapping.Element);
            var statements = new List<string>();
            foreach (var field in Model.Fields.Where(x => x.InternalElement.IsMapped))
            {
                if (field.TypeReference.Element.SpecializationTypeId != DTOModel.SpecializationTypeId)
                {
                    statements.Add($"dto.{field.Name.ToCamelCase()} = {entity.Name.ToCamelCase()}.{GetPath(field.Mapping.Path)};");
                }
                else if (field.TypeReference.IsCollection)
                {
                    statements.Add($"dto.{field.Name.ToCamelCase()} = {entity.Name.ToCamelCase()}.{GetPath(field.Mapping.Path)}?.map(x => {GetTypeName(TemplateId, field.TypeReference.Element)}.from{new DTOModel((IElement)field.TypeReference.Element).Mapping.Element.Name}(x));");
                }
                else
                {
                    statements.Add($"dto.{field.Name.ToCamelCase()} = {GetTypeName(TemplateId, field.TypeReference.Element)}.from{new DTOModel((IElement)field.TypeReference.Element).Mapping.Element.Name}({entity.Name.ToCamelCase()}.{GetPath(field.Mapping.Path)});");
                }
            }
            return string.Join(@"
    ", statements);
        }

        private static string GetPath(IEnumerable<IElementMappingPathTarget> path)
        {
            return string.Join("?.", path
                .Where(x => x.Specialization != GeneralizationModel.SpecializationType)
                .Select(x => x.Specialization == OperationModel.SpecializationType ? $"{x.Name.ToCamelCase()}()" : x.Name.ToCamelCase()));
        }

        public IEnumerable<string> GetRequiredRelations()
        {
            return GetInnerRequiredRelations(new HashSet<ICanBeReferencedType>(), this.Model);
        }

        private IEnumerable<string> GetInnerRequiredRelations(ISet<ICanBeReferencedType> haveBeenVisited, DTOModel rootDto)
        {
            var relations = Model.Fields
                .Where(f => f.InternalElement.IsMapped)
                .SelectMany(f =>
                {
                    var relation = string.Join(".", f.Mapping.Path
                        .Where(p => p.Specialization == AssociationModel.SpecializationType)
                        .Select(p => p.Name.ToCamelCase()));

                    if (f.TypeReference.Element.SpecializationTypeId != DTOModel.SpecializationTypeId)
                    {
                        haveBeenVisited.Add(f.TypeReference.Element);
                        return new[] { relation };
                    }

                    if (haveBeenVisited.Contains(f.TypeReference.Element))
                    {
                        Logging.Log.Warning($"Required relations for {rootDto.Name} detected a circular reference.");
                        haveBeenVisited.Add(f.TypeReference.Element);
                        return new[] { relation };
                    }

                    // This is the reason why this method is not a local function
                    var dtoModelTemplate = GetTemplate<DtoModelTemplate>(TemplateId, f.TypeReference.Element)
                        .GetInnerRequiredRelations(haveBeenVisited, rootDto)
                        .Select(x => $"{relation}.{x}");

                    haveBeenVisited.Add(f.TypeReference.Element);
                    return new[] { relation }.Concat(dtoModelTemplate);
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct();
            return relations;
        }

        private static bool IsDate(IHasTypeReference field)
        {
            return field.TypeReference.Element.Name is "date" or "datetime" or "datetimeoffset";
        }

        private string GetAbstractDefinition() => Model.IsAbstract
            ? " abstract"
            : string.Empty;

        public string GetBaseType()
        {
            var typeReference = Model.ParentDtoTypeReference;

            return typeReference != null
                ? $" extends {GetTypeName(typeReference)}"
                : string.Empty;
        }

        private static string GetGenericTypeParameters(IEnumerable<string> genericTypes)
        {
            var distinctGenericTypes = genericTypes.Distinct().ToArray();
            if (distinctGenericTypes.Length == 0)
            {
                return string.Empty;
            }

            return $"<{string.Join(", ", distinctGenericTypes)}>";
        }
    }
}