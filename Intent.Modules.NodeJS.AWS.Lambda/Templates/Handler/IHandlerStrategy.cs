using System.Collections.Generic;
using System.Linq;
using Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler;

internal interface IHandlerStrategy
{
    bool IsApplicable();
    string GetEventType();
    string GetReturnType();
    string GetReturnValue(string resultVariableName);
    IEnumerable<string> GetBeforeControllerHandleStatements();
    IEnumerable<string> GetControllerHandleArguments();

    static IHandlerStrategy ResolveFor(HandlerTemplate template)
    {
        IEnumerable<IHandlerStrategy> Enumerate()
        {
            yield return new ApiGatewayStrategy(template);
            yield return new DefaultStrategy(template); // This should always be last, it is the default if none of the other strategies are applicable
        }

        return Enumerate().First(x => x.IsApplicable());
    }
}