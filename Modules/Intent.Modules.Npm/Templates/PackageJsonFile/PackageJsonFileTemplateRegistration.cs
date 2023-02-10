using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.Modules.Npm.Templates.PackageJsonFile;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.SingleFileNoModel", Version = "1.0")]

namespace Intent.Modules.Npm.Templates.PackageJsonFile
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class PackageJsonFileTemplateRegistration : SingleFileTemplateRegistration
    {
        public override string TemplateId => PackageJsonFileTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget)
        {
            return new PackageJsonFileTemplate(outputTarget);
        }
    }
}