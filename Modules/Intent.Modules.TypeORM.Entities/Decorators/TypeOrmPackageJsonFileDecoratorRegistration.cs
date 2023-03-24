using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.Npm.Templates.PackageJsonFile;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Decorators
{
    [Description(TypeOrmPackageJsonFileDecorator.DecoratorId)]
    public class TypeOrmPackageJsonFileDecoratorRegistration : DecoratorRegistration<PackageJsonFileTemplate, PackageJsonFileDecorator>
    {
        public override PackageJsonFileDecorator CreateDecoratorInstance(PackageJsonFileTemplate template, IApplication application)
        {
            return new TypeOrmPackageJsonFileDecorator(template, application);
        }

        public override string DecoratorId => TypeOrmPackageJsonFileDecorator.DecoratorId;
    }
}