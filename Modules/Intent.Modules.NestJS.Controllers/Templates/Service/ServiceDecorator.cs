using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.Services.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Ignore)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecoratorContract", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.Service
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public abstract class ServiceDecorator : ITemplateDecorator
    {
        public int Priority { get; protected set; } = 0;

        public virtual IEnumerable<string> GetConstructorParameters() => Enumerable.Empty<string>();

        public virtual string GetImplementation(OperationModel operation) => null;
    }
}