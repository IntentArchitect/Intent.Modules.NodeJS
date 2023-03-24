using System;
using System.Collections.Generic;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class StackTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return TypescriptFile.ToString();
        }
    }
}