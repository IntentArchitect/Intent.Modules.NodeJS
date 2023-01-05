using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;
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
        private IReadOnlyCollection<IControllerDependencyResolver> _dependencyResolvers;

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Modules.NodeJS.AWS.Lambda.Handler";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public HandlerTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Lambda.Api.LambdaFunctionModel model) : base(TemplateId, outputTarget, model)
        {
            _handlerStrategy = IHandlerStrategy.ResolveFor(this);
            _dependencyResolvers = IControllerDependencyResolver.ResolveFor(this);

            AddTypeSource(SchemaTemplate.TemplateId);
            AddDependency(NpmPackageDependencies.AwsLambda);
            AddDependency(NpmPackageDependencies.JsonSchemaToTs);
            AddDependency(NpmPackageDependencies.Types.AwsLambda);
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();
            foreach (var dependencyResolver in _dependencyResolvers)
            {
                dependencyResolver.BeforeTemplateExecution();
            }

            _dependencyResolvers = _dependencyResolvers
                .Where(x => x.IsApplicable())
                .ToArray();
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: "handler",
                fileName: "handler",
                relativeLocation: Model.Name.ToKebabCase()
            );
        }
    }
}