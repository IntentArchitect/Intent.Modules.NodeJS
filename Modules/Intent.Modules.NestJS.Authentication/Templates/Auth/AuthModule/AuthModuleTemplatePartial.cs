using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Authentication.Events;
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
        private readonly HashSet<string> _providers = new();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Auth.AuthModule";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public AuthModuleTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(Core.NpmPackageDependencies.NestJs.Common);
            AddDependency(Core.NpmPackageDependencies.NestJs.Config);
            AddDependency(NpmPackageDependencies.NestJsJwt);
            AddDependency(NpmPackageDependencies.NestJsPassport);
            ExecutionContext.EventDispatcher.Subscribe<NestAuthModuleProviderRequest>(Handle);
        }

        private void Handle(NestAuthModuleProviderRequest request)
        {
            _providers.Add(request.Resolve(this));
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            ExecutionContext.EventDispatcher.Publish(new EnvironmentVariableRequest(EnvironmentVariables.JwtKey, EnvironmentVariables.JwtKeyDefaultValue));
            ExecutionContext.EventDispatcher.Publish(NestJsModuleImportRequest
                .Create(null, ClassName)
                .AddDependency(TemplateDependency.OnTemplate(this)));
        }

        private string GetProviders()
        {
            var providers = Enumerable.Empty<string>()
                .Append(this.GetAuthServiceName())
                .Append(this.GetLocalStrategyName())
                .Append(this.GetJwtStrategyName())
                .Concat(_providers)
                .Select(provider => $"{Environment.NewLine}    {provider}");

            return string.Join(",", providers);
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