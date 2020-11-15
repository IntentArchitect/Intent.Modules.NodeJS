using Intent.Engine;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.SingleFileNoModel", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.Main
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class MainTemplateRegistration : SingleFileTemplateRegistration
    {
        public override string TemplateId => Main.MainTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget project)
        {
            return new Main.MainTemplate(project, null);
        }
    }
}