using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TableItemRepositoriesTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.TableItemRepositories";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TableItemRepositoriesTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.DynamoDB.Api.DynamoDBTableModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var classname = $"{Model.Name.RemoveSuffix("Table")}TableItemRepositories";

            return new TypeScriptFileConfig(
                className: classname.ToPascalCase(),
                fileName: classname.ToKebabCase(),
                relativeLocation: Model.Name.ToKebabCase()
            );
        }

    }
}