using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.Strategies;

internal class DynamoDbTableStrategy : IControllerDependencyResolver
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly IElement _dynamoDbElement;
    private IClassProvider _repositoryTemplate;

    public DynamoDbTableStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _dynamoDbElement = _template.Model.InternalElement.AssociatedElements
            .Select(x => x.Association.TargetEnd.TypeReference.Element)
            .FirstOrDefault(x => x.IsDynamoDb()) as IElement;
    }

    public void BeforeTemplateExecution()
    {
        if (_dynamoDbElement == null)
        {
            return;
        }

        _template.TryGetTemplate(References.Roles.DynamoDbRepositories, _dynamoDbElement.Id, out _repositoryTemplate);
    }

    public bool IsApplicable() =>
        _dynamoDbElement != null &&
        _repositoryTemplate != null;

    public IEnumerable<string> GetConstructorArguments()
    {
        yield return $"new {_template.GetTypeName(_repositoryTemplate)}()";
    }

    public IEnumerable<string> GetConstructorParameters()
    {
        yield return $"{_repositoryTemplate.ClassName.ToCamelCase()}: {_template.GetTypeName(_repositoryTemplate)}";
    }
}