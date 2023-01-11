using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyResolvers;

internal class DynamoDbTableStrategy : IControllerDependencyResolver
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly IElement _dynamoDbElement;
    private Lazy<IClassProvider> _repositoryTemplate;

    public DynamoDbTableStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _dynamoDbElement = _template.Model.InternalElement.AssociatedElements
            .Select(x => x.Association.TargetEnd.TypeReference.Element)
            .FirstOrDefault(x => x.IsDynamoDb()) as IElement;
        _repositoryTemplate = new Lazy<IClassProvider>(() => _template.TryGetTemplate<IClassProvider>(
            Constants.Role.DynamoDbRepositories, _dynamoDbElement.Id,
            out var repositoryTemplate)
            ? repositoryTemplate
            : null);
    }

    public void BeforeTemplateExecution()
    {
        if (_dynamoDbElement == null)
        {
            return;
        }

        _template.TryGetTemplate(Constants.Role.DynamoDbRepositories, _dynamoDbElement.Id, out _repositoryTemplate);
    }

    private bool IsApplicable()
    {
        return _dynamoDbElement != null &&
               _repositoryTemplate.Value != null;
    }

    public IEnumerable<string> GetConstructorArguments()
    {
        if (!IsApplicable())
        {
            yield break;
        }

        yield return $"new {_template.GetTypeName(_repositoryTemplate.Value)}()";
    }

    public IEnumerable<string> GetConstructorParameters()
    {
        if (!IsApplicable())
        {
            yield break;
        }

        yield return $"{_repositoryTemplate.Value.ClassName.ToCamelCase()}: {_template.GetTypeName(_repositoryTemplate.Value)}";
    }
}