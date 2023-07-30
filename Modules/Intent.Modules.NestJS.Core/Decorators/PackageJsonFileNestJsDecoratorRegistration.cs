using System.ComponentModel;
using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.Modules.Npm.Templates.PackageJsonFile;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorRegistration", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Decorators
{
    [Description(PackageJsonFileNestJsDecorator.DecoratorId)]
    public class PackageJsonFileNestJsDecoratorRegistration : DecoratorRegistration<PackageJsonFileTemplate, PackageJsonFileDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public override PackageJsonFileDecorator CreateDecoratorInstance(PackageJsonFileTemplate template, IApplication application)
        {
            return new PackageJsonFileNestJsDecorator(template, application);
        }

        public override string DecoratorId => PackageJsonFileNestJsDecorator.DecoratorId;
    }
}