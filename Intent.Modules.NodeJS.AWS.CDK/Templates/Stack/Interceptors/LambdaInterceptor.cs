using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class LambdaInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _stackTemplate;

        public LambdaInterceptor(StackTemplate stackTemplate)
        {
            _stackTemplate = stackTemplate;
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _stackTemplate.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.LambdaFunction)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("aws_iam", "aws-cdk-lib");
                constructor.Class.File.AddImport("*", "lambda", "aws-cdk-lib/aws-lambda");
                constructor.Class.File.AddImport("NodejsFunction", "aws-cdk-lib/aws-lambda-nodejs");
                constructor.Class.File.AddImport("join", "path");
            }

            foreach (var resource in resources)
            {
                var template = _stackTemplate.GetTemplate<IClassProvider>(Constants.Role.LambdaHandler, resource, new TemplateDiscoveryOptions
                {
                    TrackDependency = false,
                    ThrowIfNotFound = false
                });

                if (template == null)
                {
                    continue;
                }

                var variableName = $"{resource.Name.ToCamelCase()}Function";
                var relativePath = _stackTemplate.GetRelativePath(template);
                var exportedTypeName = template.ClassName;

                constructor.AddStatement(@$"const {variableName} = new NodejsFunction(this, '{resource.Name.ToPascalCase()}Handler', {{
            entry: join(__dirname, '{relativePath}'),
            handler: '{exportedTypeName}',
            runtime: lambda.Runtime.NODEJS_16_X
        }});", statement => statement
                    .SeparatedFromPrevious()
                    .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                    .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                );
                foreach (var statement in GetAddToRolePolicyStatements(resource, variableName))
                {
                    constructor.AddStatement(statement);
                }
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            var lambdaFunctions = _stackTemplate.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.LambdaFunction)
                .OrderBy(x => x.Name)
                .ToArray();

            var statementsByElement = constructor.Statements
                .Where(x => x.HasMetadata(Constants.MetadataKey.SourceElement))
                .ToDictionary(
                    x => x.GetMetadata(Constants.MetadataKey.SourceElement),
                    x => new
                    {
                        VariableName = x.GetMetadata(Constants.MetadataKey.VariableName) as string,
                        EnvironmentVariables = x.Metadata.TryGetValue(Constants.MetadataKey.EnvironmentVariables, out var value)
                            ? (Dictionary<string, string>)value
                            : null
                    });

            foreach (var lambdaFunction in lambdaFunctions)
            {
                if (!statementsByElement.TryGetValue(lambdaFunction, out var statement))
                {
                    continue;
                }

                var lambdaVariable = statement.VariableName;
                var associationTargets = lambdaFunction.AssociatedElements
                    .Where(x => x.IsTargetEnd())
                    .Select(x => (IElement)x.Association.TargetEnd.TypeReference.Element);
                var associationSources = lambdaFunction.AssociatedElements
                    .Where(x => x.IsSourceEnd())
                    .Select(x => (IElement)x.Association.SourceEnd.TypeReference.Element);

                foreach (var resource in associationTargets)
                {
                    if (!statementsByElement.TryGetValue(resource, out var resourceStatement))
                    {
                        continue;
                    }

                    if (resourceStatement.EnvironmentVariables != null)
                    {
                        foreach (var (variable, value) in resourceStatement.EnvironmentVariables)
                        {
                            constructor.AddStatement($"{lambdaVariable}.addEnvironment('{variable}', {value});");
                        }
                    }
                }

                foreach (var resource in associationSources)
                {
                    if (!statementsByElement.TryGetValue(resource, out var resourceStatement))
                    {
                        continue;
                    }

                    if (resource.SpecializationType == Constants.ElementName.SqsQueue)
                    {
                        constructor.Class.File.AddImport("SqsEventSource", "aws-cdk-lib/aws-lambda-event-sources");
                        constructor.AddStatement($"{lambdaVariable}.addEventSource(new SqsEventSource({resourceStatement.VariableName}));");
                    }
                }
            }
        }

        private static IEnumerable<string> GetAddToRolePolicyStatements(IElement element, string variableName)
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
