using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.SingleFileNoModel", Version = "1.0")]

namespace Intent.Modules.NestJS.Authentication.Templates.Auth.Strategies.LocalStrategy
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class LocalStrategyTemplateRegistration : SingleFileTemplateRegistration
    {
        public override string TemplateId => LocalStrategyTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget)
        {
            return new LocalStrategyTemplate(outputTarget);
        }
    }
}