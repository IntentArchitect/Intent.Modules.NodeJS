using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies;

/// <summary>
/// See <see href="https://docs.aws.amazon.com/lambda/latest/dg/typescript-handler.html"/>.
/// </summary>
internal class ApiGatewayStrategy : IHandlerStrategy
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly IElement _apiGatewayElement;

    public ApiGatewayStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _apiGatewayElement = _template.Model.InternalElement.AssociatedElements
            .Select(x => x.Association.SourceEnd.TypeReference.Element)
            .FirstOrDefault(x => x.IsApiGateway()) as IElement;
    }

    public bool IsApplicable() => _apiGatewayElement != null;

    public string GetEventType() => _template.ImportType("APIGatewayEvent", "aws-lambda");

    public string GetReturnType() => _template.ImportType("APIGatewayProxyResult", "aws-lambda");

    public string GetReturnValue(string resultVariableName)
    {
        return !string.IsNullOrWhiteSpace(resultVariableName)
            ? $@"{{
        statusCode: 200,
        body: JSON.stringify({resultVariableName}),
    }}"
            : @"{
        statusCode: 200,
    }";
    }

    public IEnumerable<string> GetBeforeControllerHandleStatements() => Enumerable.Empty<string>();

    public IEnumerable<string> GetControllerHandleArguments()
    {
        var bodyParameterProcessed = false;
        var path = _apiGatewayElement.GetStereotypeProperty<string>("API Gateway Endpoint Settings", "Path") ?? string.Empty;
        var pathItems = Regex.Matches(path, "{(.*?)}")
            .Select(x => x.Value[1..^1])
            .ToHashSet();

        foreach (var parameter in _template.Model.Parameters)
        {
            if (parameter.TypeReference.Element?.SpecializationType == "DTO")
            {
                if (bodyParameterProcessed)
                {
                    throw new Exception("More than one body parameter encountered.");
                }

                bodyParameterProcessed = true;
                yield return $"event.body as {_template.GetTypeName(parameter)}";
                continue;
            }

            var resolvedType = _template.GetTypeName(_apiGatewayElement);
            var sourceField = pathItems.Contains(parameter.Name)
                ? "pathParameters"
                : "queryStringParameters";

            var converter = resolvedType switch
            {
                "number" => "Number",
                "boolean" => "Boolean",
                "string" => "String",
                _ => throw new NotSupportedException($"\"{parameter.Name}\" of resolved type \"{resolvedType}\" is not a supported type. Only DTOs, numbers, booleans and strings are supported.")
            };

            yield return $"{converter}(event.{sourceField}[\"{parameter.Name}\"])";
        }
    }
}