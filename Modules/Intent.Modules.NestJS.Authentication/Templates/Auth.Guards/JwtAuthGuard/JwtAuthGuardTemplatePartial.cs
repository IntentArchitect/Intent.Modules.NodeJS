using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Guards.JwtAuthGuard
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class JwtAuthGuardTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Auth.Guards.JwtAuthGuard";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public JwtAuthGuardTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(Core.NpmPackageDependencies.NestJs.Common);
            AddDependency(Core.NpmPackageDependencies.NestJs.Core);
            AddDependency(NpmPackageDependencies.Passport);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"JwtAuthGuard",
                fileName: $"jwt-auth.guard"
            );
        }

    }
}