using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class LambdaInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _stackTemplate;

        public LambdaInterceptor(StackTemplate stackTemplate)
        {
            _stackTemplate = stackTemplate;
        }

        public void Apply(TypescriptConstructor constructor)
        {
            var lambdaFunctions = _stackTemplate.Model.UnderlyingPackage.GetChildElementsOfType(References.Elements.LambdaFunction)
                .OrderBy(x => x.Name)
                .ToArray();

            if (lambdaFunctions.Length > 0)
            {
                constructor.Class.File.AddImport("aws_iam", "aws-cdk-lib");
                constructor.Class.File.AddImport("*", "lambda", "aws-cdk-lib/aws-lambda");
                constructor.Class.File.AddImport("NodejsFunction", "aws-cdk-lib/aws-lambda-nodejs");
                constructor.Class.File.AddImport("join", "path");
            }

            foreach (var lambdaFunction in lambdaFunctions)
            {
                var handlerTemplate = _stackTemplate.GetTemplate<IClassProvider>("Distribution.Functions.Handler", lambdaFunction, new TemplateDiscoveryOptions
                {
                    TrackDependency = false
                });
                var variableName = $"{lambdaFunction.Name.ToCamelCase()}Function";
                var relativePath = _stackTemplate.GetRelativePath(handlerTemplate);
                var exportedTypeName = handlerTemplate.ClassName;

                constructor.AddStatement(@$"const {variableName} = new NodejsFunction(this, '{lambdaFunction.Name.ToPascalCase()}Handler', {{
            entry: join(__dirname, '{relativePath}'),
            handler: '{exportedTypeName}',
            runtime: lambda.Runtime.NODEJS_16_X
        }});", statement => statement
                    .SeparatedFromPrevious()
                    .AddMetadata("SourceElement", lambdaFunction)
                    .AddMetadata("VariableName", variableName)
                );
                foreach (var statement in GetAddToRolePolicyStatements(lambdaFunction, variableName))
                {
                    constructor.AddStatement(statement);
                }
            }
        }

        private IEnumerable<string> GetAddToRolePolicyStatements(IElement element, string variableName)
        {
            var policies = element
                .OwnedAssociations.SelectMany(x => x.TargetEnd.ChildElements)
                .Where(x => x.IsIAMPolicyStatementReferenceModel())
                .Select(x => x.AsIAMPolicyStatementReferenceModel().TypeReference.Element?.AsIAMPolicyStatementModel())
                .ToList();

            foreach (var policy in policies)
            {
                yield return @$"{variableName}.addToRolePolicy(new aws_iam.PolicyStatement({{
            actions: [{string.Concat(policy.Actions.Select(x => QuotedName(x.Name)))}
            ],
            resources: [{string.Concat(policy.Resources.Select(x => QuotedName(x.Name)))}
            ],
        }}));";
            }

            static string QuotedName(string name)
            {
                return name[0] is '\'' or '"' or '`'
                    ? $"{Environment.NewLine}                {name},"
                    : $"{Environment.NewLine}                '{name}',";
            }
        }
    }
}
