using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.RepositoryTemplate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class RepositoryTemplate : TypeScriptTemplateBase<ClassModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.TypeORM.Entities.RepositoryTemplate";

        public RepositoryTemplate(IOutputTarget outputTarget, ClassModel model) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.ToPascalCase()}Repository",
                fileName: $"{Model.Name.ToKebabCase()}.repository"
            );
        }

    }
}