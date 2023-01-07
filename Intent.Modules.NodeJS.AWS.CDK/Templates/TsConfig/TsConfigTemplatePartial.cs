using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.TsConfig
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TsConfigTemplate : IntentTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.CDK.TsConfig";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TsConfigTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.Types.Node);
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.AwsCdk);
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.AwsCdkLib);
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.TsNode);
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.Typescript);
        }

        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TemplateFileConfig(
                fileName: "tsconfig",
                fileExtension: "json",
                overwriteBehaviour: OverwriteBehaviour.OnceOff
            );
        }
    }
}