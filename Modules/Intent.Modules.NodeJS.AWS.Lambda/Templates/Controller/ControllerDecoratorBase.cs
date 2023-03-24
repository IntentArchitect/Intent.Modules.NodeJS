using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorContract", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public abstract class ControllerDecoratorBase : ITemplateDecorator
    {
        public int Priority { get; protected set; } = 0;

        public virtual string GetImplementation() => string.Empty;

    }
}