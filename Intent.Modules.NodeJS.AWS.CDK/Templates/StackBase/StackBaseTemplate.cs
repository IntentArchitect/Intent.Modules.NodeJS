using System;
using System.Collections.Generic;
using Intent.Modules.TypeScript.Weaving.Decorators;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.StackBase
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class StackBaseTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ Stack }} from 'aws-cdk-lib'
import {{ Construct }} from 'constructs'

export class {ClassName} extends Stack {{
    // {this.IntentIgnoreBodyDecorator()}
    constructor (scope: Construct, id: string, props?: {this.GetStackBasePropsName()}) {{
        super(scope, id, props);
    }}
}}
";
        }
    }
}