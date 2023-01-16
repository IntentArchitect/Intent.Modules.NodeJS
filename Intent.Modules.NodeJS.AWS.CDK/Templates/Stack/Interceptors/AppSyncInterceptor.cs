using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class AppSyncInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _template;

        public AppSyncInterceptor(StackTemplate template)
        {
            _template = template;
        }

        // https://docs.aws.amazon.com/cdk/api/v2/docs/aws-cdk-lib.aws_appsync-readme.html
        public void ApplyInitial(TypescriptConstructor constructor)
        {
            var resources = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.GraphQLEndpoint)
                .OrderBy(x => x.Name)
                .ToArray();

            if (resources.Length > 0)
            {
                constructor.Class.File.AddImport("*", "appsync", "aws-cdk-lib/aws-appsync");
            }

            foreach (var resource in resources)
            {
                var template = _template.GetTemplate<ITemplate>("Lib.GraphQL.Schema", resource, new TemplateDiscoveryOptions
                {
                    TrackDependency = false,
                    ThrowIfNotFound = false
                });

                if (template == null)
                {
                    continue;
                }

                var variableName = $"{resource.Name.ToCamelCase().RemoveSuffix("GraphQL", "Endpoint")}GraphQL";
                var relativePath = _template.GetRelativePath(template);

                constructor.AddStatement($@"const {variableName} = new appsync.GraphqlApi(this, '{resource.Name}', {{
            name: '{resource.Name}',
            schema: appsync.SchemaFile.fromAsset(join(__dirname, '{relativePath}')),
            authorizationConfig: {{
                defaultAuthorization: {{
                    authorizationType: appsync.AuthorizationType.IAM,
                }},
            }},
        }});", statement =>
                {
                    statement
                        .SeparatedFromPrevious()
                        .AddMetadata(Constants.MetadataKey.SourceElement, resource)
                        .AddMetadata(Constants.MetadataKey.VariableName, variableName)
                        .AddMetadata(Constants.MetadataKey.EnvironmentVariables, new Dictionary<string, string>());
                });
            }
        }

        public void ApplyPost(TypescriptConstructor constructor)
        {
            var graphQls = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.GraphQLEndpoint)
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

            foreach (var graphQl in graphQls)
            {
                var appsyncVarName = statementsByElement[graphQl].VariableName;

                var elementsMappedToLambdas = graphQl.ChildElements
                    .SelectMany(x => x.ChildElements)
                    .Where(x =>
                        x.SpecializationType is Constants.ElementName.GraphQLMutation or Constants.ElementName.GraphQLSchemaField &&
                        x.MappedElement?.Element is IElement { SpecializationType: Constants.ElementName.LambdaFunction });
                foreach (var element in elementsMappedToLambdas)
                {
                    var lambdaVarName = statementsByElement[(IElement)element.MappedElement.Element].VariableName;

                    constructor.AddStatement($@"{appsyncVarName}
            .addLambdaDataSource('{element.Name.RemoveSuffix("Lambda")}LambdaDataSource', {lambdaVarName})
            .createResolver('{element.Name}Resolver', {{
                typeName: '{element.ParentElement.Name}',
                fieldName: '{element.Name}',
                requestMappingTemplate: appsync.MappingTemplate.lambdaRequest(),
                responseMappingTemplate: appsync.MappingTemplate.lambdaResult()
            }});
");
                }

                var elementsMappedToEntities = graphQl.ChildElements
                    .Where(x =>
                        x.SpecializationType is Constants.ElementName.GraphQLMutation or Constants.ElementName.GraphQLSchemaField &&
                        x.MappedElement?.Element is IElement { SpecializationType: Constants.ElementName.Entity });
                foreach (var element in elementsMappedToEntities)
                {
                    
                }
            }
        }
    }
}
