using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Ignore)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorContract", Version = "1.0")]

namespace Intent.Modules.Npm.Templates.PackageJsonFile
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public abstract class PackageJsonFileDecorator : ITemplateDecorator
    {
        public int Priority { get; protected set; } = 0;

        public abstract void UpdateSettings(JsonEditor fileEditor);
    }
}