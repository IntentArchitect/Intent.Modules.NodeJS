using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Users.UserContext
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class UserContextTemplate : TypeScriptTemplateBase<object, UserContextDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Authentication.Users.UserContext";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public UserContextTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        private IEnumerable<string> GetDecoratorMembers()
        {
            return GetDecorators().SelectMany(x => x.GetMembers());
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"UserContext",
                fileName: $"user-context"
            );
        }

    }
}