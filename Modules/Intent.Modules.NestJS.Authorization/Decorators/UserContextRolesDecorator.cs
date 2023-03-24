using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.NestJS.Authentication.Templates.Users.UserContext;
using Intent.Modules.NestJS.Authorization.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class UserContextRolesDecorator : UserContextDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.NestJS.Authorization.UserContextRolesDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly UserContextTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public UserContextRolesDecorator(UserContextTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override IEnumerable<string> GetMembers()
        {
            yield return $"roles?: {_template.GetRoleEnumName()}[]";

            foreach (var member in base.GetMembers())
            {
                yield return member;
            }
        }
    }
}