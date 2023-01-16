using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Message;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Schema;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class HandlerTemplate : TypeScriptTemplateBase<Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel, HandlerDecoratorBase>
    {
        private readonly IHandlerStrategy _handlerStrategy;
        private readonly IReadOnlyCollection<IControllerDependencyProvider> _dependencyResolvers;

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.Lambda.Handler";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public HandlerTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel model) : base(TemplateId, outputTarget, model)
        {
            _handlerStrategy = IHandlerStrategy.ResolveFor(this);
            _dependencyResolvers = IControllerDependencyProvider.GetFor(this);

            AddTypeSource(MessageTemplate.TemplateId);
            AddDependency(NpmPackageDependencies.AwsLambda);
            AddDependency(NpmPackageDependencies.Types.AwsLambda);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: "main",
                fileName: "handler",
                relativeLocation: this.GetSubstitutedRelativePath(Model.InternalElement.Package, Model.Name.ToKebabCase())
            );
        }
    }
}