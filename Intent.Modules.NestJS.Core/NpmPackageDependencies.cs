using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NestJS.Core;

public static class NpmPackageDependencies
{
    public static NpmPackageDependency NestJsConfig = new("@nestjs/config", "^2.2.0");
}