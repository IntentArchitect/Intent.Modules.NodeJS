using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack
{
    [IntentManaged(Mode.Merge, Signature = Mode.Merge)]
    partial class StackTemplate : TypeScriptTemplateBase<AWSPackageModel>, ITypescriptFileBuilderTemplate
    {
        private readonly IReadOnlyCollection<IStackTemplateInterceptor> _interceptors;

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.AWS.CDK.Stack";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public StackTemplate(IOutputTarget outputTarget, Intent.Modelers.AWS.Api.AWSPackageModel model) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Publish(NpmPackageDependencies.AwsCdkLib);
            _interceptors = IStackTemplateInterceptor.GetFor(this);
        }

        public TypescriptFile TypescriptFile { get; private set; }

        public override void AfterTemplateRegistration()
        {
            base.AfterTemplateRegistration();

            TypescriptFile = new TypescriptFile(this.GetFolderPath())
                    .AddImport("Construct", "constructs")
                    .AddClass(ClassName, @class => @class
                        .Export()
                        .ExtendsClass(this.GetApplicationStackName())
                        .AddConstructor(constructor =>
                        {
                            constructor
                                .AddParameter("scope", "Construct")
                                .AddParameter("id", "string")
                                .AddParameter("props", this.GetApplicationStackPropsName(), param => param
                                    .Optional()
                                )
                                .CallsSuper(super => super
                                    .AddArguments("scope", "id", "props")
                                )
                                ;

                            foreach (var interceptor in _interceptors)
                            {
                                interceptor.ApplyInitial(constructor);
                            }

                            foreach (var interceptor in _interceptors)
                            {
                                interceptor.ApplyPost(constructor);
                            }
                        })
                    )
                ;
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            var className = Model.Name.RemoveSuffix("Stack");

            return new TypeScriptFileConfig(
                className: $"{className.ToPascalCase()}Stack",
                fileName: $"{className.ToSnakeCase()}-stack"
            );
        }
    }
}