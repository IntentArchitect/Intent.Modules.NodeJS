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
import * as apigateway from 'aws-cdk-lib/aws-apigateway';
import * as lambda from 'aws-cdk-lib/aws-lambda';

type ResourceApi = {{
    name: string;
    methods: {{
        verb: string,
        lambda: lambda.IFunction,
    }}[];
    resources: ResourceApi[]
}}

export class {ClassName} extends Stack {{
    // {this.IntentIgnoreBodyDecorator()}
    constructor (scope: Construct, id: string, props?: {this.GetStackBasePropsName()}) {{
        super(scope, id, props);
    }}

    protected applyApi(target: apigateway.IResource, source: ResourceApi): void {{
        for (const sourceMethod of source.methods) {{
            target.addMethod(sourceMethod.verb, new apigateway.LambdaIntegration(sourceMethod.lambda));
        }}
    
        for (const sourceResource of source.resources) {{
            this.applyApi(target.addResource(sourceResource.name), sourceResource);
        }}
    }}
}}
";
        }
    }
}