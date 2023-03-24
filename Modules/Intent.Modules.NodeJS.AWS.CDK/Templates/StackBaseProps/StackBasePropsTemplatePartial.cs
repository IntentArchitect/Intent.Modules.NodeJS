using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.StackBaseProps
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class StackBasePropsTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.CDK.StackBaseProps";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public StackBasePropsTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.AwsCdkLib);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var appName = ExecutionContext.GetApplicationConfig().Name;

            return new TypeScriptFileConfig(
                className: $"{appName.ToPascalCase()}StackProps",
                fileName: $"{appName.ToKebabCase()}-stack-props"
            );
        }

    }
}