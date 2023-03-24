using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Validation.Decorators
{
    [Description(DtoValidationDecorator.DecoratorId)]
    public class DtoValidationDecoratorRegistration : DecoratorRegistration<DtoModelTemplate, DtoModelDecorator>
    {
        public override DtoModelDecorator CreateDecoratorInstance(DtoModelTemplate template, IApplication application)
        {
            return new DtoValidationDecorator(template, application);
        }

        public override string DecoratorId => DtoValidationDecorator.DecoratorId;
    }
}