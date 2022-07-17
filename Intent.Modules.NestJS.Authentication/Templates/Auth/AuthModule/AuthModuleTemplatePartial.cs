using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Auth.AuthModule
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class AuthModuleTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Auth.AuthModule";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public AuthModuleTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(Core.NpmPackageDependencies.NestJs.Common);
            AddDependency(Core.NpmPackageDependencies.NestJs.Config);
            AddDependency(NpmPackageDependencies.NestJsJwt);
            AddDependency(NpmPackageDependencies.NestJsPassport);
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            ExecutionContext.EventDispatcher.Publish(new EnvironmentVariableRequest(EnvironmentVariables.JwtKey, EnvironmentVariables.JwtKeyDefaultValue));
            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest
                .Create(null, ClassName)
                .AddDependency(TemplateDependency.OnTemplate(this)));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"AuthModule",
                fileName: $"auth.module"
            );
        }

    }
}