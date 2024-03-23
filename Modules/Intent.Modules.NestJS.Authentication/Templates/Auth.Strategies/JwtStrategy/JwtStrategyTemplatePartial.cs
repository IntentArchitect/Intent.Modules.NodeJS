using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.JwtStrategy
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class JwtStrategyTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Auth.Strategies.JwtStrategy";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public JwtStrategyTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(Core.NpmPackageDependencies.NestJs.Common);
            AddDependency(Core.NpmPackageDependencies.NestJs.Config);
            AddDependency(NpmPackageDependencies.Passport);
            AddDependency(NpmPackageDependencies.PassportJwt);
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            ExecutionContext.EventDispatcher.Publish(new EnvironmentVariableRequest(EnvironmentVariables.JwtKey, EnvironmentVariables.JwtKeyDefaultValue));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"JwtStrategy",
                fileName: $"jwt-strategy"
            );
        }

    }
}