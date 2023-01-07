using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Middyfy
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class MiddyfyTemplate : TypeScriptTemplateBase<object, MiddyfyDecoratorBase>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Middyfy";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public MiddyfyTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.Middy.Core);
            AddDependency(NpmPackageDependencies.Middy.HttpJsonBodyParser);
            AddDependency(NpmPackageDependencies.TypeFest);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = "middyfy";

            return new TypeScriptFileConfig(
                className: className,
                fileName: className
            );
        }

    }
}