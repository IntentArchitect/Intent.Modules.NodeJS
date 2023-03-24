using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.SQS.Templates.SqsSender
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class SqsSenderTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.SQS.SqsSender";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public SqsSenderTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.AwsSdk.ClientSqs);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"SqsSender",
                fileName: $"sqs-sender"
            );
        }

    }
}