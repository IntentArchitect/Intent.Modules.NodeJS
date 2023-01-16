using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Message;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class ControllerTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel, ControllerDecoratorBase>
    {
        private readonly IReadOnlyCollection<IControllerDependencyProvider> _dependencyResolvers;

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Controller";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public ControllerTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(MessageTemplate.TemplateId);
            _dependencyResolvers = IControllerDependencyProvider.GetFor(this);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.ToPascalCase()}Controller",
                fileName: $"{Model.Name.ToKebabCase()}-controller",
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package, Model.Name.ToKebabCase())
            );
        }
    }
}