using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepository
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityRepositoryTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.DynamoDB.Api.EntityModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.EntityRepository";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EntityRepositoryTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.DynamoDB.Api.EntityModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.ToPascalCase()}Repository",
                fileName: $"{Model.Name.ToKebabCase()}-repository",
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package, Model.GetTable().Name.ToKebabCase())
            );
        }

    }
}