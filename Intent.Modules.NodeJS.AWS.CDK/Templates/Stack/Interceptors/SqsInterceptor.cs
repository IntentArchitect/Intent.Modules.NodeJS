﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class SqsInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _template;

        public SqsInterceptor(StackTemplate template)
        {
            _template = template;
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.SqsQueue)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("*", "sqs", "aws-cdk-lib/aws-sqs");
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.ToCamelCase().RemoveSuffix("Sqs", "Queue")}SqsQueue";
                var environmentVariableName = $"{variableName}Url".ToSnakeCase().ToUpperInvariant();

                constructor.Class.AddField(variableName, "sqs.Queue", field => field.PrivateReadOnly());
                constructor.AddStatement($@"this.{variableName} = new sqs.Queue(this, '{resource.Name}');", statement =>
                {
                    statement
                        .SeparatedFromPrevious()
                        .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                        .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                        .AddMetadata(Constants.MetadataKey.SqsQueueUrl, environmentVariableName)
                        .AddMetadata(Constants.MetadataKey.EnvironmentVariables, new Dictionary<string, string>
                        {
                            [environmentVariableName] = $"this.{variableName}.queueUrl"
                        });
                });
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            var queues = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.SqsQueue)
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

            foreach (var queue in queues)
            {
                if (!statementsByElement.TryGetValue(queue, out var statement))
                {
                    return;
                }

                var queueVariable = statement.VariableName;
                var associationSources = queue.AssociatedElements
                    .Where(x => x.IsSourceEnd())
                    .Select(x => (IElement)x.Association.SourceEnd.TypeReference.Element)
                    .ToArray();

                foreach (var resource in associationSources)
                {
                    if (!statementsByElement.TryGetValue(resource, out var resourceStatement))
                    {
                        continue;
                    }

                    if (resource.SpecializationType == Constants.ElementName.LambdaFunction)
                    {
                        constructor.AddStatement($"this.{queueVariable}.grantSendMessages(this.{resourceStatement.VariableName});");
                    }
                }
            }
        }
    }
}
