using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Auth.AuthController
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class AuthControllerTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Auth.AuthController";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public AuthControllerTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"AuthController",
                fileName: $"auth-controller"
            );
        }

    }
}