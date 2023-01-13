using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyProviders;

internal class DynamoDbTableProvider : IControllerDependencyProvider
{
    private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
    private readonly IElement _dynamoDbElement;
    private readonly Lazy<IClassProvider> _repositoryTemplate;

    public DynamoDbTableProvider(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        _template = template;
        _dynamoDbElement = _template.Model.InternalElement.AssociatedElements
            .Select(x => x.Association.TargetEnd.TypeReference.Element as IElement)
            .FirstOrDefault(x => x?.SpecializationType == Constants.ElementName.DynamoDbTable);
        _repositoryTemplate = new Lazy<IClassProvider>(() => _template.TryGetTemplate<IClassProvider>(
            Constants.Role.DynamoDbRepositories, _dynamoDbElement.Id,
            out var repositoryTemplate)
            ? repositoryTemplate
            : null);
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