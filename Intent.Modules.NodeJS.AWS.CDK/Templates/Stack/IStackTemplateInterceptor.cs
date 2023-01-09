using System.Collections.Generic;
using System.Linq;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack;

internal interface IStackTemplateInterceptor
{
    void Apply(TypescriptConstructor constructor);

    static IReadOnlyCollection<IStackTemplateInterceptor> GetFor(StackTemplate template)
    {
        return new IStackTemplateInterceptor[]
            {
                new LambdaInterceptor(template)
            }
            .ToArray();
    }
}