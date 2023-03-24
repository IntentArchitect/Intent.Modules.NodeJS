using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FileTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.NpmIgnore
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class NpmIgnoreTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return @$"*.ts
!*.d.ts

# CDK asset staging directory
.cdk.staging
cdk.out
";
        }
    }
}