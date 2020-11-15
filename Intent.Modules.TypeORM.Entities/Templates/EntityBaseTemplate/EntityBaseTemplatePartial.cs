using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;


[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.EntityBaseTemplate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityBaseTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.TypeORM.Entities.EntityBaseTemplate";

        public EntityBaseTemplate(IOutputTarget outputTarget, object model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"EntityBase",
                fileName: $"base.entity"
            );
        }

    }
}