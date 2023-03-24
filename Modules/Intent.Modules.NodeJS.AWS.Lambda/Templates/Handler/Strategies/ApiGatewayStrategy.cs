using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies;

/// <summary>
/// See <see href="https://docs.aws.amazon.com/lambda/latest/dg/typescript-handler.html"/>.
/// </summary>
internal class ApiGatewayStrategy : IHandlerStrategy
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly IElement _associationSourceElement;

    public ApiGatewayStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _associationSourceElement = _template.Model.InternalElement.AssociatedElements
            .Select(x => x.Association.SourceEnd.TypeReference.Element as IElement)
            .FirstOrDefault(x => x?.SpecializationType == Constants.ElementName.ApiMethod);
    }

    public bool IsApplicable() => _associationSourceElement != null;

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
        body: '',
    }";
    }

    public IEnumerable<string> GetBeforeControllerHandleStatements() => Enumerable.Empty<string>();
    public void ApplyControllerCall(StringBuilder stringBuilder, string resultAssignment)
    {
        IHandlerStrategy.DefaultApplyControllerCall(stringBuilder, resultAssignment, GetArguments());
    }

    private IEnumerable<string> GetArguments()
    {
        var bodyParameterProcessed = false;
        var pathItems = PathParts(_associationSourceElement.ParentElement)
            .SelectMany(
                part => Regex.Matches(part, "{(.*?)}"),
                (_, match) => match.Value[1..^1])
            .ToHashSet();

        foreach (var parameter in _template.Model.Parameters)
        {
            if (parameter.TypeReference.Element?.SpecializationType == Constants.ElementName.Message)
            {
                if (bodyParameterProcessed)
                {
                    throw new Exception("More than one body parameter encountered.");
                }

                bodyParameterProcessed = true;
                yield return "event.body as any";
                continue;
            }

            var resolvedType = _template.GetTypeName(parameter);
            var sourceField = pathItems.Contains(parameter.Name)
                ? "pathParameters"
                : "queryStringParameters";

            var converter = resolvedType switch
            {
                "number" => "Number",
                "boolean" => "Boolean",
                "string" => "String",
                _ => throw new NotSupportedException($"\"{parameter.Name}\" of resolved type \"{resolvedType}\" is not a supported type. Only Messages, numbers, booleans and strings are supported.")
            };

            yield return $"{converter}(event.{sourceField}?.['{parameter.Name}'])";
        }

        static IEnumerable<string> PathParts(IElement element)
        {
            while (element.SpecializationType == Constants.ElementName.ApiResource)
            {
                yield return element.Name;
                element = element.ParentElement;
            }
        }
    }
}