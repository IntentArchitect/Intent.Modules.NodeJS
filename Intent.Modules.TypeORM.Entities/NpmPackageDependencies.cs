using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.TypeORM.Entities;

public static class NpmPackageDependencies
{
    public static class NestJs
    {
        public static NpmPackageDependency TypeOrm = new("@nestjs/typeorm", "^9.0.0");
    }

    public static NpmPackageDependency TypeOrm = new("typeorm", "^0.3.7");
    public static NpmPackageDependency EnvVar = new("env-var", "^7.1.1");
}