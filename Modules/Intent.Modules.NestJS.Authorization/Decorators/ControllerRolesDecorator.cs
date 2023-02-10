using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.WebApi.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Authentication.Events;
using Intent.Modules.NestJS.Authorization.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Controller;
using Intent.Modules.NestJS.Core;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NestJS.Authorization.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class ControllerRolesDecorator : ControllerDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.NestJS.Authorization.ControllerRolesDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly ControllerTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public ControllerRolesDecorator(ControllerTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override IEnumerable<string> GetClassDecorators()
        {
            if (_template.Model.HasSecured() && !string.IsNullOrWhiteSpace(_template.Model.GetSecured().Roles()))
            {
                var roles = _template.Model
                    .GetSecured()
                    .Roles()
                    .Split(",")
                    .Select(role => $"{_template.GetRoleEnumName()}.{role.Trim()}");

                yield return $"@{_template.GetRolesDecoratorName()}({string.Join(", ", roles)})";
            }

            foreach (var decorator in base.GetClassDecorators())
            {
                yield return decorator;
            }
        }

        public override IEnumerable<string> GetOperationDecorators(OperationModel operation)
        {
            if (operation.HasSecured() && !string.IsNullOrWhiteSpace(operation.GetSecured().Roles()))
            {
                var roles = operation
                    .GetSecured()
                    .Roles()
                    .Split(",")
                    .Select(role => $"{_template.GetRoleEnumName()}.{role.Trim()}");

                yield return $"@{_template.GetRolesDecoratorName()}({string.Join(", ", roles)})";
            }

            foreach (var decorator in base.GetOperationDecorators(operation))
            {
                yield return decorator;
            }
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            if (!string.IsNullOrWhiteSpace(_template.Model.GetSecured()?.Roles()) ||
                _template.Model.Operations.Any(operation => !string.IsNullOrWhiteSpace(operation.GetSecured()?.Roles())))
            {
                _application.EventDispatcher.Publish(new NestAuthModuleProviderRequest(template =>
                {
                    template.AddDependency(NpmPackageDependencies.NestJs.Core);

                    return @$"{{
      provide: {template.ImportType("APP_GUARD", "@nestjs/core")},
      useClass: {template.GetRolesGuardName()},
    }}";
                }));
            }
        }
    }
}