using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Api;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;
using Intent.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyProviders
{
    internal class SqsQueueProvider : IControllerDependencyProvider
    {
        private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
        private readonly Lazy<IReadOnlyCollection<Queue>> _queues;

        public SqsQueueProvider(TypeScriptTemplateBase<LambdaFunctionModel> template)
        {
            _template = template;
            _queues = new Lazy<IReadOnlyCollection<Queue>>(() => GetQueues().ToArray());
        }

        public IEnumerable<string> GetConstructorParameters() => _queues.Value
            .Select(x => $"{x.ParameterName}: {x.TypeName}");

        public IEnumerable<string> GetConstructorArguments() => _queues.Value
            .Select(x => $"new {x.TypeName}(process.env.{x.UrlEnvironmentVariable} as string)");

        private IEnumerable<Queue> GetQueues()
        {
            if (!_template.TryGetTemplate(
                    templateId: Constants.Role.Stacks, 
                    model: _template.Model.InternalElement.Package, 
                    template: out ITypescriptFileBuilderTemplate stackTemplate,
                    trackDependencies: false) ||
                !_template.TryGetTemplate(
                    templateId: Constants.Role.SqsSender, 
                    template: out ITemplate sqsSenderTemplate,
                    trackDependencies: false))
            {
                yield break;
            }

            var sqsSenderTypeName = new Lazy<string>(() => _template.GetTypeName(sqsSenderTemplate));
            var statementsByElement = stackTemplate.TypescriptFile.Classes[0].Constructors[0].Statements
                .Where(x => x.HasMetadata(Constants.MetadataKey.SourceElement))
                .ToDictionary(
                    x => x.GetMetadata(Constants.MetadataKey.SourceElement),
                    x => new
                    {
                        Statement = x,
                        EnvironmentVariables = x.Metadata.TryGetValue(Constants.MetadataKey.EnvironmentVariables, out var value)
                            ? (Dictionary<string, string>)value
                            : null
                    });

            var queueElements = _template.Model.InternalElement.AssociatedElements
                .Select(x => x.Association.TargetEnd.TypeReference.Element)
                .Where(x => x is IElement { SpecializationType: Constants.ElementName.SqsQueue })
                .Cast<IElement>();

            foreach (var queue in queueElements)
            {
                if (!statementsByElement.TryGetValue(queue, out var statement) ||
                    !statement.Statement.TryGetMetadata(Constants.MetadataKey.SqsQueueUrl, out var value))
                {
                    continue;
                }

                var urlEnvVarName = value as string;

                var subscribers = queue.AssociatedElements
                    .Select(x => x.Association.TargetEnd.TypeReference.Element)
                    .Where(x => x is IElement { SpecializationType: Constants.ElementName.LambdaFunction })
                    .Cast<IElement>();

                var messageTypes = string.Join(" | ", subscribers
                    .Select(x => x.ChildElements
                        .FirstOrDefault(y =>
                            y.SpecializationType == Constants.ElementName.Parameter &&
                            y.TypeReference.Element.SpecializationType == Constants.ElementName.Message))
                    .Where(x => x != null)
                    .Select(x => _template.GetMessageName(x.TypeReference.Element.AsMessageModel())));

                yield return new Queue
                {
                    ParameterName = $"{queue.Name.ToCamelCase()}Sender",
                    TypeName = $"{sqsSenderTypeName.Value}<{messageTypes}>",
                    UrlEnvironmentVariable = urlEnvVarName
                };
            }
        }

        private sealed class Queue
        {
            public string ParameterName { get; set; }
            public string TypeName { get; set; }
            public string UrlEnvironmentVariable { get; set; }
        }
    }
}
