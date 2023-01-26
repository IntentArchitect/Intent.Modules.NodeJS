using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors;

internal class ApiGatewayInterceptor : IStackTemplateInterceptor
{
    private readonly StackTemplate _template;

    public ApiGatewayInterceptor(StackTemplate template)
    {
        _template = template;
    }

    public void ApplyInitial(TypescriptConstructor constructor)
    {
        var resources = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.ApiGateway)
            .OrderBy(x => x.Name)
            .ToArray();

        if (resources.Length > 0)
        {
            constructor.Class.File.AddImport("*", "apigateway", "aws-cdk-lib/aws-apigateway");
        }

        // https://docs.aws.amazon.com/cdk/api/v2/docs/aws-cdk-lib.aws_apigateway-readme.html
        // https://github.dev/aws-samples/serverless-patterns/blob/main/apigw-lambda-dynamodb-xray/cdk/lib/apigw-lambda-dynamodb-cdk-ts-stack.ts
        foreach (var resource in resources)
        {
            var variableName = resource.Name.ToCamelCase().EnsureSuffixedWith("ApiGateway", "Api", "Gateway");

            constructor.Class.AddField(variableName, "apigateway.RestApi", field => field.PrivateReadOnly());
            constructor.AddStatement($@"this.{variableName} = new apigateway.RestApi(this, '{resource.Name}', {{
            deployOptions: {{
                dataTraceEnabled: true,
                tracingEnabled: true,
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
        var apis = _template.Model.UnderlyingPackage.GetChildElementsOfType(Constants.ElementName.ApiGateway)
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
        var variableNamesByElement = statementsByElement.ToDictionary(x => x.Key, x => x.Value.VariableName);

        foreach (var api in apis)
        {
            if (!statementsByElement.TryGetValue(api, out var statements))
            {
                return;
            }

            var variableName = statements.VariableName;
            var generatedApi = new StringBuilder();
            GenerateApi(generatedApi, api, variableNamesByElement, "        ");
            constructor.AddStatement($"this.applyApi(this.{variableName}.root, {generatedApi});");
        }
    }

    private static void GenerateApi(
        StringBuilder stringBuilder,
        IElement element,
        Dictionary<object, string> elementToVariableNameMap,
        string indentation)
    {
        const string tab = "  ";

        stringBuilder.AppendLine("{");
        indentation += tab;
        {
            var name = element.SpecializationType == Constants.ElementName.ApiResource
                ? element.Name
                : string.Empty;

            stringBuilder.AppendLine($"{indentation}name: '{name}',");
            stringBuilder.Append($"{indentation}methods: [");
            indentation += tab;
            var hadMethod = false;

            foreach (var method in element.ChildElements.Where(x => x.SpecializationType == Constants.ElementName.ApiMethod))
            {
                hadMethod = true;
                var verb = method.TypeReference.Element?.Name ?? "UNKNOWN";
                var lambda = method.OwnedAssociations
                    .Select(x => x.TargetEnd.TypeReference.Element)
                    .OfType<IElement>()
                    .FirstOrDefault(x => x.SpecializationType == Constants.ElementName.LambdaFunction);
                if (lambda == null ||
                    !elementToVariableNameMap.TryGetValue(lambda, out var variableName))
                {
                    variableName = "UNKNOWN";
                }

                stringBuilder.Append($"{Environment.NewLine}{indentation}{{ verb: '{verb}', lambda: this.{variableName} }},");
            }

            indentation = indentation[..^tab.Length];
            if (hadMethod)
            {
                stringBuilder.Append($"{Environment.NewLine}{indentation}");
            }
            stringBuilder.AppendLine("],");

            stringBuilder.Append($"{indentation}resources: [");
            indentation += tab;
            var hadResource = false;

            foreach (var resource in element.ChildElements.Where(x => x.SpecializationType == Constants.ElementName.ApiResource))
            {
                hadResource = true;
                stringBuilder.Append($"{Environment.NewLine}{indentation}");
                GenerateApi(stringBuilder, resource, elementToVariableNameMap, indentation);
                stringBuilder.Append(',');
            }

            indentation = indentation[..^tab.Length];
            if (hadResource)
            {
                stringBuilder.Append($"{Environment.NewLine}{indentation}");
            }
            stringBuilder.AppendLine("]");
        }
        indentation = indentation[..^tab.Length];
        stringBuilder.Append($"{indentation}}}");
    }
}