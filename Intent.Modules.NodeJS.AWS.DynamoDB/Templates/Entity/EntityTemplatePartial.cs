using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.Entity
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.DynamoDB.Api.EntityModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.Entity";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EntityTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.DynamoDB.Api.EntityModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = $"{Model.Name}";

            return new TypeScriptFileConfig(
                className: className,
                fileName: className.ToKebabCase(),
                relativeLocation: Model.GetTable().Name.ToKebabCase()
            );
        }

    }
}