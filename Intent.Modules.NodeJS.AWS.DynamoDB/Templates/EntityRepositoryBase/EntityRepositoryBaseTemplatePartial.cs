using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepositoryBase
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityRepositoryBaseTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.DynamoDB.EntityRepositoryBase";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EntityRepositoryBaseTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.AwsSdk.LibDynamodb);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = "EntityRepositoryBase";

            return new TypeScriptFileConfig(
                className: className,
                fileName: className.ToKebabCase()
            );
        }

    }
}