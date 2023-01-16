using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Message
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class MessageTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.Api.MessageModel, PayloadDecoratorBase>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Message";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public MessageTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Api.MessageModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = Model.Name;

            return new TypeScriptFileConfig(
                className: className.ToPascalCase(),
                fileName: className.ToKebabCase(),
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package)
            );
        }

    }
}