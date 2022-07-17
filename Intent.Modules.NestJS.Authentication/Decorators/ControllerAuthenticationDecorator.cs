using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.WebApi.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.NestJS.Authentication.Templates;
using Intent.Modules.NestJS.Controllers.Templates.Controller;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class ControllerAuthenticationDecorator : ControllerDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.NestJS.Authentication.ControllerAuthenticationDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly ControllerTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public ControllerAuthenticationDecorator(ControllerTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override IEnumerable<string> GetClassDecorators()
        {
            if (_template.Model.HasSecured())
            {
                _template.ImportType("ApiBearerAuth", "@nestjs/swagger");
                yield return "@ApiBearerAuth()";

                _template.ImportType("UseGuards", "@nestjs/common");
                yield return $"@UseGuards({_template.GetJwtAuthGuardName()})";
            }

            foreach (var decorator in base.GetClassDecorators())
            {
                yield return decorator;
            }
        }

        public override IEnumerable<string> GetOperationDecorators(OperationModel operation)
        {
            if (operation.HasSecured())
            {
                _template.ImportType("ApiBearerAuth", "@nestjs/swagger");
                yield return "@ApiBearerAuth()";

                _template.ImportType("UseGuards", "@nestjs/common");
                yield return $"@UseGuards({_template.GetJwtAuthGuardName()})";
            }

            if (operation.HasUnsecured() && _template.Model.HasSecured())
            {
                yield return $"@{_template.GetPublicDecoratorName()}()";
            }

            foreach (var decorator in base.GetOperationDecorators(operation))
            {
                yield return decorator;
            }
        }

        public override void BeforeTemplateExecution()
        {
            base.BeforeTemplateExecution();

            if (_template.Model.HasSecured() || _template.Model.Operations.Any(x => x.HasSecured()))
            {
                _application.EventDispatcher.Publish(new NestSwaggerDocumentBuilderRequest(".addBearerAuth()"));
            }
        }
    }
}