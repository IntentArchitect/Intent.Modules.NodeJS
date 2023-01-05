using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies;

internal class DefaultStrategy : IHandlerStrategy
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly ParameterModel _parameter;

    public DefaultStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _parameter = _template.Model.Parameters.Count switch
        {
            0 => null,
            1 => _template.Model.Parameters[0],
            _ => throw new Exception("Expected 0 or 1 parameters.")
        };
    }

    public bool IsApplicable() => true;

    public string GetEventType() => _parameter != null
        ? _template.GetTypeName(_parameter)
        : "any";

    public string GetReturnType() => _template.Model.TypeReference.Element != null
        ? _template.GetTypeName(_template.Model.TypeReference)
        : "void";

    public string GetReturnValue(string resultVariableName) => null;

    public IEnumerable<string> GetBeforeControllerHandleStatements() => Enumerable.Empty<string>();

    public IEnumerable<string> GetControllerHandleArguments()
    {
        if (_parameter != null)
        {
            yield return $"event as {_template.GetTypeName(_parameter)}";
        }
    }
}