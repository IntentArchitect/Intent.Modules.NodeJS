using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modelers.Services.Api;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
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

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DtoModelTemplate(IOutputTarget outputTarget, Intent.Modelers.Services.Api.DTOModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
            AddTypeSource(EntityTemplate.TemplateId);

            if (Model.Fields.Any(IsDate))
            {
                AddDependency(NpmPackageDependencies.ClassTransformer);
            }
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.RemoveSuffix("DTO", "Dto").ToKebabCase()}.dto"
            );
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

            yield return $"@{ImportType("ApiProperty", "@nestjs/swagger")}()";

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
            return GetInnerRequiredRelations(new HashSet<ICanBeReferencedType>(), 0);
        }

        // As I was going through the Pet Clinic sample and there were missing Types in the Services designer. I accidentally
        // specified the type for the PetDTO's Visits to be VisitDTO instead of PetVisitDTO. This caused an infinite recursion
        // because it would try to navigate between OwnerDTO to PetDTO and from PetDTO to OwnerDTO.
        // I thought it was a bug. So I think I solved the circular reference check using this algorithm and also then
        // picked up what the correct DTO was supposed to be. I'm keeping this code since users could have a case where
        // DTOs map in a circular way and this will resolve safely and only go 1 level deep.
        private IEnumerable<string> GetInnerRequiredRelations(ISet<ICanBeReferencedType> haveBeenVisited, int curLevel)
        {
            var relations = Model.Fields
                .Where(f => f.InternalElement.IsMapped)
                .SelectMany(f =>
                {
                    var relation = string.Join(".", f.Mapping.Path
                        .Where(p => p.Specialization == AssociationModel.SpecializationType)
                        .Select(p => p.Name.ToCamelCase()));
                    
                    if (f.TypeReference.Element.SpecializationTypeId != DTOModel.SpecializationTypeId
                        || haveBeenVisited.Contains(f.TypeReference.Element))
                    {
                        haveBeenVisited.Add(f.TypeReference.Element);
                        return new[] { relation };
                    }

                    if (curLevel > 1)
                    {
                        return Array.Empty<string>();
                    }
                    
                    var dtoModelTemplate = GetTemplate<DtoModelTemplate>(TemplateId, f.TypeReference.Element)
                        .GetInnerRequiredRelations(haveBeenVisited, curLevel + 1)
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
    }
}