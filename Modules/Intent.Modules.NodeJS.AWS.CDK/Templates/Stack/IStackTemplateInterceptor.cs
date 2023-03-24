using System.Collections.Generic;
using System.Linq;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack;

internal interface IStackTemplateInterceptor
{
    void ApplyInitial(TypescriptConstructor constructor);
    void ApplyPost(TypescriptConstructor constructor);

    static IReadOnlyCollection<IStackTemplateInterceptor> GetFor(StackTemplate template)
    {
        return new IStackTemplateInterceptor[]
            {
                new LambdaInterceptor(template),
                new DynamoDbInterceptor(template),
                new SqsInterceptor(template),
                new S3Interceptor(template),
                new AppSyncInterceptor(template),
                new ApiGatewayInterceptor(template),
                new StepFunctionsInterceptor(template)
            }
            .ToArray();
    }
}