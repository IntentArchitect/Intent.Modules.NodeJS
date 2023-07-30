using System.ComponentModel;
using Intent.Engine;
using Intent.Module.TypeScript.Domain.Templates.Entity;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Decorators
{
    [Description(EntityTypeOrmDecorator.DecoratorId)]
    public class EntityTypeOrmDecoratorRegistration : DecoratorRegistration<EntityTemplate, EntityDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public override EntityDecorator CreateDecoratorInstance(EntityTemplate template, IApplication application)
        {
            return new EntityTypeOrmDecorator(template, application);
        }

        public override string DecoratorId => EntityTypeOrmDecorator.DecoratorId;
    }
}