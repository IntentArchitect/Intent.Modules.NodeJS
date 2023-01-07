using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepository
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TableItemRepositoryTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.TableItemRepository";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TableItemRepositoryTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBItemModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = Model.Name.RemoveSuffix("Item").EnsureSuffixedWith("ItemRepository");

            return new TypeScriptFileConfig(
                className: className,
                fileName: className.ToKebabCase(),
                relativeLocation: Model.GetTable().Name.ToKebabCase()
            );
        }

    }
}