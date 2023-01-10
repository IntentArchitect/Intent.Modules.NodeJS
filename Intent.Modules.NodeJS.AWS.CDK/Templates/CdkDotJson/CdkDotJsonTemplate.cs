using System;
using System.Collections.Generic;
using System.IO;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.CDK.Templates.EntryPoint;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.FileTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.CdkDotJson
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class CdkDotJsonTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            var entryPointTemplate = GetTemplate<ITemplate>(EntryPointTemplate.TemplateId);

            return @$"{{
  ""app"": ""npx ts-node --prefer-ts-exts {this.GetRelativePath(entryPointTemplate)}"",
  ""build"": ""npx tsc"",
  ""watch"": {{
    ""include"": [
      ""**""
    ],
    ""exclude"": [
      ""README.md"",
      ""cdk*.json"",
      ""**/*.d.ts"",
      ""**/*.js"",
      ""tsconfig.json"",
      ""package*.json"",
      ""yarn.lock"",
      ""node_modules"",
      ""test""
    ]
  }},
  ""context"": {{
    ""@aws-cdk/aws-lambda:recognizeLayerVersion"": true,
    ""@aws-cdk/core:checkSecretUsage"": true,
    ""@aws-cdk/core:target-partitions"": [
      ""aws"",
      ""aws-cn""
    ],
    ""@aws-cdk-containers/ecs-service-extensions:enableDefaultLogDriver"": true,
    ""@aws-cdk/aws-ec2:uniqueImdsv2TemplateName"": true,
    ""@aws-cdk/aws-ecs:arnFormatIncludesClusterName"": true,
    ""@aws-cdk/aws-iam:minimizePolicies"": true,
    ""@aws-cdk/core:validateSnapshotRemovalPolicy"": true,
    ""@aws-cdk/aws-codepipeline:crossAccountKeyAliasStackSafeResourceName"": true,
    ""@aws-cdk/aws-s3:createDefaultLoggingPolicy"": true,
    ""@aws-cdk/aws-sns-subscriptions:restrictSqsDescryption"": true,
    ""@aws-cdk/aws-apigateway:disableCloudWatchRole"": true,
    ""@aws-cdk/core:enablePartitionLiterals"": true,
    ""@aws-cdk/aws-events:eventsTargetQueueSameAccount"": true,
    ""@aws-cdk/aws-iam:standardizedServicePrincipals"": true,
    ""@aws-cdk/aws-ecs:disableExplicitDeploymentControllerForCircuitBreaker"": true
  }}
}}
";
        }
    }
}