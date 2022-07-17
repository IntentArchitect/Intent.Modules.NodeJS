using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NestJS.Authentication.Events;

public class NestAuthModuleProviderRequest
{
    public delegate string ResolveDelegate(TypeScriptTemplateBase<object> template);
    private readonly ResolveDelegate _resolveDelegate;

    public NestAuthModuleProviderRequest(ResolveDelegate resolveDelegate)
    {
        _resolveDelegate = resolveDelegate;
    }

    public string Resolve(TypeScriptTemplateBase<object> template) => _resolveDelegate(template);
}