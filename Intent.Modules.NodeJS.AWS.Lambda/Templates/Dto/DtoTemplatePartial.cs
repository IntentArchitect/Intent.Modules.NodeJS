using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Dto
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class DtoTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.Api.DTOModel, PayloadDecoratorBase>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Dto";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DtoTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Api.DTOModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = Model.Name;

            return new TypeScriptFileConfig(
                className: className.ToPascalCase(),
                fileName: className.ToKebabCase()
            );
        }

    }
}