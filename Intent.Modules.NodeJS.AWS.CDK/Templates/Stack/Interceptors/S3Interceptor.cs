using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class S3Interceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _template;

        public S3Interceptor(StackTemplate template)
        {
            _template = template;
        }

        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.S3Bucket)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("*", "s3", "aws-cdk-lib/aws-s3");
            }

            foreach (var resource in resources)
            {
                var variableName = $"{resource.Name.ToCamelCase().EnsureSuffixedWith("Bucket")}";
                var environmentVariableName = $"{variableName.ToSnakeCase().ToUpperInvariant()}_NAME";
                var options = string.Concat(Options(resource).Select(x => $"{Environment.NewLine}            {x},"));

                constructor.AddStatement($@"const {variableName} = new s3.Bucket(this, '{resource.Name}', {{{options}
        }});", statement =>
                {
                    statement
                        .SeparatedFromPrevious()
                        .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                        .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                        .AddMetadata(Constants.MetadataKey.S3BucketName, environmentVariableName)
                        .AddMetadata(Constants.MetadataKey.EnvironmentVariables, new Dictionary<string, string>
                        {
                            [environmentVariableName] = $"{variableName}.bucketName"
                        });
                });
            }

            IEnumerable<string> Options(IElement element)
            {
                var removalPolicy = element.GetStereotypeProperty<string>(
                    Constants.Stereotype.S3BucketSettings.Name,
                    Constants.Stereotype.S3BucketSettings.Property.RemovalPolicy);
                if (!string.IsNullOrWhiteSpace(removalPolicy))
                {
                    constructor.Class.File.AddImport("RemovalPolicy", "aws-cdk-lib");
                    yield return $"removalPolicy: RemovalPolicy.{removalPolicy.ToUpperInvariant()}";
                }

                var versioned = element.GetStereotypeProperty<bool>(
                    Constants.Stereotype.S3BucketSettings.Name,
                    Constants.Stereotype.S3BucketSettings.Property.Versioned).ToString().ToLowerInvariant();
                yield return $"versioned: {versioned}";
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            var buckets = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.S3Bucket)
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

            foreach (var bucket in buckets)
            {
                if (!statementsByElement.TryGetValue(bucket, out var statements))
                {
                    return;
                }

                var queueVariable = statements.VariableName;
                var associationSources = bucket.AssociatedElements
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
                        constructor.AddStatement($"{queueVariable}.grantDelete({resourceStatement.VariableName});");
                        constructor.AddStatement($"{queueVariable}.grantPut({resourceStatement.VariableName});");
                        constructor.AddStatement($"{queueVariable}.grantReadWrite({resourceStatement.VariableName});");
                    }
                }
            }
        }
    }
}
