using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyResolvers;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;

internal interface IControllerDependencyResolver
{
    IEnumerable<string> GetConstructorArguments();
    IEnumerable<string> GetConstructorParameters();
    void BeforeTemplateExecution();

    static IReadOnlyCollection<IControllerDependencyResolver> GetFor(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        return new IControllerDependencyResolver[]
        {
            new DynamoDbTableStrategy(template)
        }
        .ToArray();
    }
}