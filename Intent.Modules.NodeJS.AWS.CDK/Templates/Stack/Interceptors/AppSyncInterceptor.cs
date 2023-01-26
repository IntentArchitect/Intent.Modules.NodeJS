using System;
using System.Collections.Generic;
using System.Linq;
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
                constructor.Class.File.AddImport("join", "path");
            }

            foreach (var resource in resources)
            {
                var template = _template.GetTemplate<ITemplate>(Constants.Role.GraphQlSchema, resource, new TemplateDiscoveryOptions
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

                constructor.Class.AddField(variableName, "appsync.GraphqlApi", field => field.PrivateReadOnly());
                constructor.AddStatement($@"this.{variableName} = new appsync.GraphqlApi(this, '{resource.Name}', {{
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
                        Element = x,
                        VariableName = x.GetMetadata(Constants.MetadataKey.VariableName) as string,
                        EnvironmentVariables = x.Metadata.TryGetValue(Constants.MetadataKey.EnvironmentVariables, out var value)
                            ? (Dictionary<string, string>)value
                            : null

                    });

            foreach (var graphQl in graphQls)
            {
                if (!statementsByElement.TryGetValue(graphQl, out var statement))
                {
                    return;
                }

                var appsyncVarName = statement.VariableName;

                var mappedElements = graphQl.ChildElements
                    .SelectMany(x => x.ChildElements)
                    .Where(x =>
                        x.SpecializationType is Constants.ElementName.GraphQLMutation or Constants.ElementName.GraphQLSchemaField &&
                        x.MappedElement?.Element is IElement)
                    .Select(x => (element: x, mappedElement: (IElement)x.MappedElement.Element))
                    .OrderBy(x => x.mappedElement.SpecializationType)
                    .ToArray();

                foreach (var entityElement in mappedElements
                             .Select(x => x.mappedElement)
                             .Where(x => x.SpecializationType == Constants.ElementName.Entity)
                             .Distinct())
                {
                    var dataSourceVarName = DataSourceVarName(entityElement);
                    var tableVarName = statementsByElement[entityElement.ParentElement].VariableName;
                    constructor.AddStatement($"const {dataSourceVarName} = {appsyncVarName}.addDynamoDbDataSource('{dataSourceVarName}', this.{tableVarName});");
                }

                foreach (var (element, mappedElement) in mappedElements)
                {
                    static string TypeName(IElement element) =>
                        element.ParentElement.SpecializationType switch
                        {
                            Constants.ElementName.GraphQLMutationType or Constants.ElementName.GraphQLQueryType => element.ParentElement.Name,
                            _ => element.ParentElement.Name.EnsureSuffixedWith("Type")
                        };

                    // To Lambdas
                    if (mappedElement.SpecializationType == Constants.ElementName.LambdaFunction)
                    {
                        var lambdaVarName = statementsByElement[(IElement)element.MappedElement.Element].VariableName;
                        var namePrefix = $"{element.ParentElement.Name.ToCamelCase()}{element.Name.ToPascalCase().RemoveSuffix("Lambda")}";

                        constructor.AddStatement($@"{appsyncVarName}
            .addLambdaDataSource('{namePrefix}LambdaDataSource', {lambdaVarName})
            .createResolver('{namePrefix}Resolver', {{
                typeName: '{TypeName(element)}',
                fieldName: '{element.Name.ToCamelCase()}',
                requestMappingTemplate: appsync.MappingTemplate.lambdaRequest(),
                responseMappingTemplate: appsync.MappingTemplate.lambdaResult()
            }});");
                    }

                    // To DynamoDb
                    if (mappedElement.SpecializationType == Constants.ElementName.Entity)
                    {
                        if (!TryGetConvention(element, mappedElement, out var convention))
                        {
                            continue;
                        }

                        var dataSourceVarName = DataSourceVarName(mappedElement);
                        var typeName = element.ParentElement.Name;
                        var fieldName = element.Name;
                        var parameter = element.ChildElements
                            .SingleOrDefault(x => x.SpecializationType == Constants.ElementName.GraphQLParameter);
                        var partitionKey = mappedElement.ParentElement.ChildElements
                            .SingleOrDefault(x => x.SpecializationType == Constants.ElementName.PartitionKey);
                        var primaryKeyName = partitionKey?.Name.ToCamelCase() ?? "UNKNOWN";
                        var parameterName = parameter?.Name ?? "UNKNOWN";
                        var idFieldName = (parameter?.TypeReference.Element as IElement)?.ChildElements
                            .SingleOrDefault(x => x.SpecializationType == Constants.ElementName.GraphQLSchemaField &&
                                                  x.Name == partitionKey?.Name)?.Name.ToCamelCase() ?? "UNKNOWN";

                        var (requestMappingTemplate, responseMappingTemplate) = convention switch
                        {
                            Convention.Create => (
                                @$"appsync.MappingTemplate.dynamoDbPutItem(
                appsync.PrimaryKey.partition('{primaryKeyName}').auto(),
                appsync.Values.projecting('{parameterName}'),
            )",
                                "appsync.MappingTemplate.dynamoDbResultItem()"),
                            Convention.Delete => (
                                $"appsync.MappingTemplate.dynamoDbDeleteItem('{primaryKeyName}', '{parameterName}')",
                                "appsync.MappingTemplate.dynamoDbResultItem()"),
                            Convention.Get => (
                                $"appsync.MappingTemplate.dynamoDbGetItem('{primaryKeyName}', '{parameterName}')",
                                "appsync.MappingTemplate.dynamoDbResultItem()"),
                            Convention.List => (
                                "appsync.MappingTemplate.dynamoDbScanTable()",
                                "appsync.MappingTemplate.dynamoDbResultList()"),
                            Convention.Update => (
                                @$"appsync.MappingTemplate.dynamoDbPutItem(
                appsync.PrimaryKey.partition('{primaryKeyName}').is('{parameterName}.{idFieldName}'),
                appsync.Values.projecting('{parameterName}'),
            )",
                                "appsync.MappingTemplate.dynamoDbResultItem()"),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        constructor.AddStatement(@$"{dataSourceVarName}.createResolver('{fieldName}{typeName.ToPascalCase()}Resolver', {{
            typeName: '{TypeName(element)}',
            fieldName: '{fieldName}',
            requestMappingTemplate: {requestMappingTemplate},
            responseMappingTemplate: {responseMappingTemplate},
        }});");

                    }
                }
            }

            string DataSourceVarName(IElement entityElement) => $"{statementsByElement[entityElement.ParentElement].VariableName}DataSource";
        }

        private bool TryGetConvention(IElement graphQl, IElement entity, out Convention convention)
        {
            var graphQlName = graphQl.Name.ToLowerInvariant();
            var parameters = graphQl.ChildElements
                .Where(x => x.SpecializationType == Constants.ElementName.GraphQLParameter)
                .ToArray();

            // TODO: Also check mapped to of parameter type aligns
            if ((graphQlName.Contains("add") || graphQlName.Contains("create")) &&
                parameters.Length == 1)
            {
                convention = Convention.Create;
                return true;
            }

            // TODO: Also check mapped to of parameter type aligns
            if ((graphQlName.Contains("update") || graphQlName.Contains("edit")) &&
                parameters.Length == 1)
            {
                convention = Convention.Update;
                return true;
            }

            // TODO: Also check mapped to of return type and parameter type aligns and return type is collection
            if ((graphQlName.Contains("list") || graphQlName.Contains("getall")))
            {
                convention = Convention.List;
                return true;
            }

            // TODO: Also check mapped to of return type and parameter type aligns
            if ((graphQlName.Contains("get") || graphQlName.Contains("fetch")) &&
                parameters.Length == 1)
            {
                convention = Convention.Get;
                return true;
            }

            // TODO: Also check mapped to of return type and parameter type aligns
            if ((graphQlName.Contains("delete") || graphQlName.Contains("remove")) &&
                parameters.Length == 1)
            {
                convention = Convention.Delete;
                return true;
            }

            convention = default;
            return false;
        }

        private enum Convention
        {
            Create,
            Delete,
            Get,
            List,
            Update
        }
    }
}
