using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyResolvers;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;

internal interface IControllerDependencyProvider
{
    IEnumerable<string> GetConstructorArguments();
    IEnumerable<string> GetConstructorParameters();

    static IReadOnlyCollection<IControllerDependencyProvider> GetFor(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        return new IControllerDependencyProvider[]
        {
            new DynamoDbTableProvider(template)
        }
        .ToArray();
    }
}