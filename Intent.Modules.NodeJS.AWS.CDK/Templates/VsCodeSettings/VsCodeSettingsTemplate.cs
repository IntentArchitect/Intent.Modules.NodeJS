using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FileTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.VsCodeSettings
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class VsCodeSettingsTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return @$"{{
    ""explorer.excludeGitIgnore"": true
}}";
        }
    }
}