using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FileTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.TsConfig
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class TsConfigTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return @$"{{
  ""compilerOptions"": {{
    ""target"": ""ES2020"",
    ""module"": ""commonjs"",
    ""lib"": [
      ""es2020""
    ],
    ""declaration"": true,
    ""strict"": true,
    ""noImplicitAny"": true,
    ""strictNullChecks"": true,
    ""noImplicitThis"": true,
    ""alwaysStrict"": true,
    ""noUnusedLocals"": false,
    ""noUnusedParameters"": false,
    ""noImplicitReturns"": true,
    ""noFallthroughCasesInSwitch"": false,
    ""inlineSourceMap"": true,
    ""inlineSources"": true,
    ""experimentalDecorators"": true,
    ""strictPropertyInitialization"": false,
    ""typeRoots"": [
      ""./node_modules/@types""
    ]
  }},
  ""exclude"": [
    ""node_modules"",
    ""cdk.out""
  ]
}}
";
        }
    }
}