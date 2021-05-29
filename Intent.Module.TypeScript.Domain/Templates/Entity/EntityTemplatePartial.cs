using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
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
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name}",
                fileName: $"{Model.Name.ToKebabCase()}.entity");
        }

        private string GetClassDecorators()
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
  ", x.GetClassDecorators()));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"
{decorators}";
        }

        private object GetAttributeDecorators(AttributeModel attribute)
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
  ", x.GetFieldDecorators(attribute)));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"
  {decorators}";
        }

        private string GetAssociationDecorators(AssociationEndModel associationEnd)
        {
            var decorators = GetDecoratorsOutput(x => string.Join(@"
  ", x.GetFieldDecorators(associationEnd)));
            return string.IsNullOrWhiteSpace(decorators) ? "" : $@"
  {decorators}";
        }
    }
}