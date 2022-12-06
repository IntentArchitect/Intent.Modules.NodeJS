using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Module.TypeScript.Domain.Templates.Enum;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Module.TypeScript.Domain.Templates.Entity
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityTemplate : TypeScriptTemplateBase<Intent.Modelers.Domain.Api.ClassModel, EntityDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.Domain.Entity";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EntityTemplate(IOutputTarget outputTarget, Intent.Modelers.Domain.Api.ClassModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(EntityTemplate.TemplateId);
            AddTypeSource(EnumTemplate.TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.ToKebabCase()}.entity",
                relativeLocation: this.GetFolderPath());
        }

        private string GetClassDecorators()
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
", x.GetClassDecorators()));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"
{decorators}";
        }

        private string GetAttributeDecorators(AttributeModel attribute)
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
  ", x.GetFieldDecorators(attribute)));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"{decorators}
  ";
        }

        private string GetAssociationDecorators(AssociationEndModel associationEnd)
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
  ", x.GetFieldDecorators(associationEnd)));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"{decorators}
  ";
        }

        private IEnumerable<AssociationEndModel> GetAssociationsRequiringFields()
        {
            return Model.AssociatedClasses
                .Where(associationEnd =>
                    associationEnd.IsNavigable ||
                    GetDecorators()
                        .Any(decorator => decorator.RequiresAssociationFieldFor(associationEnd)));
        }

        private string GetDerivedFrom()
        {
            if (Model.ParentClass == null)
            {
                return string.Empty;
            }

            return $" extends {this.GetEntityName(Model.ParentClass)}";
        }
    }

    public static class TypeScriptCodeExtensions
    {
        public static string GetMethodParameters<TModel, TParameterModel>(this TypeScriptTemplateBase<TModel> template, IEnumerable<TParameterModel> parameters)
            where TParameterModel : IHasName, IHasTypeReference
        {
            return string.Join(", ", parameters.Select(x => $"{x.Name.ToCamelCase()}{(x.TypeReference.IsNullable ? "?" : "")}: {template.GetTypeName(x)}"));
        }

        public static string GetParameters<TModel, TParameterModel>(this TypeScriptTemplateBase<TModel> template, IEnumerable<TParameterModel> parameters)
            where TParameterModel : IHasName, IHasTypeReference
        {
            return string.Join(", ", parameters.Select(x => $"{x.Name.ToCamelCase()}{(x.TypeReference.IsNullable ? "?" : "")}: {template.GetTypeName(x)}"));
        }

        public static string GetArguments<TModel, TParameterModel>(this TypeScriptTemplateBase<TModel> template, IEnumerable<TParameterModel> parameters)
            where TParameterModel : IHasName, IHasTypeReference
        {
            return string.Join(", ", parameters.Select(x => $"{x.Name}"));
        }
    }
}