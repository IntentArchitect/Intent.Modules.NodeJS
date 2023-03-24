using System.Collections.Generic;
using System.Linq;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller.DependencyProviders;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller;

internal interface IControllerDependencyProvider
{
    IEnumerable<string> GetConstructorParameters();
    IEnumerable<string> GetConstructorArguments();

    static IReadOnlyCollection<IControllerDependencyProvider> GetFor(TypeScriptTemplateBase<LambdaFunctionModel> template)
    {
        return new IControllerDependencyProvider[]
        {
            new DynamoDbTableProvider(template),
            new SqsQueueProvider(template),
            new S3BucketProvider(template)
        }
        .ToArray();
    }
}