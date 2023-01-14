using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;
using Intent.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyProviders
{
    internal class S3BucketProvider : IControllerDependencyProvider
    {
        private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
        private readonly Lazy<IReadOnlyCollection<Bucket>> _buckets;

        public S3BucketProvider(TypeScriptTemplateBase<LambdaFunctionModel> template)
        {
            _template = template;
            _buckets = new Lazy<IReadOnlyCollection<Bucket>>(() => GetBuckets().ToArray());
        }

        public IEnumerable<string> GetConstructorParameters() => _buckets.Value
            .Select(x => $"{x.ParameterName}: {x.TypeName}");

        public IEnumerable<string> GetConstructorArguments() => _buckets.Value
            .Select(x => $"new {x.TypeName}(process.env.{x.UrlEnvironmentVariable} as string)");

        private IEnumerable<Bucket> GetBuckets()
        {
            if (!_template.TryGetTemplate(
                        templateId: Constants.Role.Stacks,
                        model: _template.Model.InternalElement.Package,
                        template: out ITypescriptFileBuilderTemplate stackTemplate,
                        trackDependencies: false) ||
                    !_template.TryGetTemplate(
                        templateId: Constants.Role.S3BucketClient,
                        template: out ITemplate s3BucketClientTemplate,
                        trackDependencies: false))
            {
                yield break;
            }

            // Lazy that we don't needlessly add the dependency
            var s3BucketClientTypeName = new Lazy<string>(() => _template.GetTypeName(s3BucketClientTemplate));
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

            var bucketElements = _template.Model.InternalElement.AssociatedElements
                .Select(x => x.Association.TargetEnd.TypeReference.Element)
                .Where(x => x is IElement { SpecializationType: Constants.ElementName.S3Bucket })
                .Cast<IElement>();

            foreach (var bucket in bucketElements)
            {
                if (!statementsByElement.TryGetValue(bucket, out var statement) ||
                    !statement.Statement.TryGetMetadata(Constants.MetadataKey.S3BucketName, out var urlEnvVarName))
                {
                    continue;
                }

                yield return new Bucket
                {
                    ParameterName = $"{bucket.Name.ToCamelCase()}BucketClient",
                    TypeName = $"{s3BucketClientTypeName.Value}",
                    UrlEnvironmentVariable = urlEnvVarName as string
                };
            }
        }

        private sealed class Bucket
        {
            public string ParameterName { get; set; }
            public string TypeName { get; set; }
            public string UrlEnvironmentVariable { get; set; }
        }
    }
}
