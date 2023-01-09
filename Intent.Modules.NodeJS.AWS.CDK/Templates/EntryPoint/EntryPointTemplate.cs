using System;
using System.Collections.Generic;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.CDK.Api;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.EntryPoint
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class EntryPointTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"//#!/usr/bin/env node
import * as cdk from 'aws-cdk-lib';

const app = new cdk.App();{string.Concat(GetStacks())}";
        }

        private IEnumerable<string> GetStacks()
        {
            foreach (var stack in ExecutionContext.MetadataManager.Application(ExecutionContext.GetApplicationConfig().Id).GetStackModels())
            {
                var stackName = this.GetStackName(stack);
                yield return $"{Environment.NewLine}new {stackName}(app, '{stackName}');";
            }
        }
    }
}