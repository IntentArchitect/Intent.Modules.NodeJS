using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemMapAttribute
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TableItemMapAttributeTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.DynamoDB.Api.MapAttributeModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.TableItemMapAttribute";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TableItemMapAttributeTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.DynamoDB.Api.MapAttributeModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = $"{Model.Name.RemoveSuffix("Map").RemoveSuffix("MapItem")}MapItem";

            return new TypeScriptFileConfig(
                className: className,
                fileName: className.ToKebabCase(),
                relativeLocation: Model.GetTable().Name.ToKebabCase()
            );
        }

    }
}