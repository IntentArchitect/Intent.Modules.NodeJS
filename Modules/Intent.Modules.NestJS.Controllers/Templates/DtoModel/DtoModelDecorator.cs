using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.Services.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorContract", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.DtoModel
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public abstract class DtoModelDecorator : ITemplateDecorator
    {
        public int Priority { get; protected set; } = 0;
        public virtual void BeforeTemplateExecution() { }
        public virtual IEnumerable<string> GetDecorators(DTOFieldModel dtoField) => Enumerable.Empty<string>();
    }
}