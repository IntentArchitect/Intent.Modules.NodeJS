using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.Services.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorContract", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.Controller
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public abstract class ControllerDecorator : ITemplateDecorator
    {
        public int Priority { get; protected set; } = 0;

        public virtual IEnumerable<string> GetClassDecorators() => Enumerable.Empty<string>();
        public virtual IEnumerable<string> GetOperationDecorators(OperationModel operation) => Enumerable.Empty<string>();
        public virtual void BeforeTemplateExecution() { }
    }
}