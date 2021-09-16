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
    partial class DtoModelTemplate : TypeScriptTemplateBase<Intent.Modelers.Services.Api.DTOModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Controllers.DtoModel";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DtoModelTemplate(IOutputTarget outputTarget, Intent.Modelers.Services.Api.DTOModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(DtoModelTemplate.TemplateId);
            AddTypeSource(EntityTemplate.TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.RemoveSuffix("DTO", "Dto").ToKebabCase()}.dto"
            );
        }

        private string GetFieldDecorators(DTOFieldModel field)
        {
            return $"@{ImportType("ApiProperty", "@nestjs/swagger")}()";
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
                    statements.Add($"dto.{field.Name.ToCamelCase()} = {entity.Name.ToCamelCase()}.{GetPath(field.Mapping.Path)}?.map(x => {GetTypeName(DtoModelTemplate.TemplateId, field.TypeReference.Element)}.from{new DTOModel((IElement)field.TypeReference.Element).Mapping.Element.Name}(x));");
                }
                else
                {
                    statements.Add($"dto.{field.Name.ToCamelCase()} = {GetTypeName(DtoModelTemplate.TemplateId, field.TypeReference.Element)}.from{new DTOModel((IElement)field.TypeReference.Element).Mapping.Element.Name}({entity.Name.ToCamelCase()}.{GetPath(field.Mapping.Path)});");
                }
            }
            return string.Join(@"
    ", statements);
        }

        private string GetPath(IEnumerable<IElementMappingPathTarget> path)
        {
            return string.Join("?.", path
                .Where(x => x.Specialization != GeneralizationModel.SpecializationType)
                .Select(x => x.Specialization == OperationModel.SpecializationType ? $"{x.Name.ToCamelCase()}()" : x.Name.ToCamelCase()));
        }

        public IEnumerable<string> GetRequiredRelations()
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
                        return new[] { relation };
                    }
                    var dtoModelTemplate = GetTemplate<DtoModelTemplate>(DtoModelTemplate.TemplateId, f.TypeReference.Element)
                        .GetRequiredRelations()
                        .Select(x => $"{relation}.{x}");
                    return new[] { relation }.Concat(dtoModelTemplate);
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct();
            return relations;
        }
    }
}