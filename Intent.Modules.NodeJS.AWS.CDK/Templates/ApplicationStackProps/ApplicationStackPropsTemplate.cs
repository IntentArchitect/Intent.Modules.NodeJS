using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Intent.Modules.TypeScript.Weaving.Decorators;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.ApplicationStackProps
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class ApplicationStackPropsTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ StackProps }} from 'aws-cdk-lib'

// {this.IntentIgnoreDecorator()}
export interface {ClassName} extends StackProps {{
}}
";
        }
    }
}