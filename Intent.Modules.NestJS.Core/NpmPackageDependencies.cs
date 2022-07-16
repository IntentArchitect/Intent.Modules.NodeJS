using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NestJS.Core;

public static class NpmPackageDependencies
{
    public static class NestJs
    {
        public static NpmPackageDependency Cli = new("@nestjs/cli", "^9.0.0");
        public static NpmPackageDependency Common = new("@nestjs/common", "^9.0.1");
        public static NpmPackageDependency Config = new("@nestjs/config", "^2.2.0");
        public static NpmPackageDependency Core = new("@nestjs/core", "^9.0.1");
        public static NpmPackageDependency PlatformExpress = new("@nestjs/platform-express", "^9.0.1");
        public static NpmPackageDependency Schematics = new("@nestjs/schematics", "^9.0.1");
        public static NpmPackageDependency Swagger = new("@nestjs/swagger", "^6.0.1");
        public static NpmPackageDependency Testing = new("@nestjs/testing", "^9.0.1");
    }

    public static class Types
    {
        public static NpmPackageDependency Express = new("@types/express", "^4.17.13");
        public static NpmPackageDependency Jest = new("@types/jest", "^28.1.4");
        public static NpmPackageDependency Node = new("@types/node", "^18.0.3");
        public static NpmPackageDependency Supertest = new("@types/supertest", "^2.0.12");
    }

    public static class TypescriptEslint
    {
        public static NpmPackageDependency EslintPlugin = new("@typescript-eslint/eslint-plugin", "^5.30.5");
        public static NpmPackageDependency Parser = new("@typescript-eslint/parser", "^5.30.5");
    }

    public static NpmPackageDependency Eslint = new("eslint", "^8.19.0");
    public static NpmPackageDependency EslintConfigPrettier = new("eslint-config-prettier", "^8.5.0");
    public static NpmPackageDependency EslintPluginPrettier = new("eslint-plugin-prettier", "^4.2.1");
    public static NpmPackageDependency Jest = new("jest", "^28.1.2");
    public static NpmPackageDependency Prettier = new("prettier", "^2.7.1");
    public static NpmPackageDependency ReflectMetadata = new("reflect-metadata", "^0.1.13");
    public static NpmPackageDependency Rimraf = new("rimraf", "^3.0.2");
    public static NpmPackageDependency Rxjs = new("rxjs", "^7.5.5");
    public static NpmPackageDependency Sqlite3 = new("sqlite3", "^5.0.2");
    public static NpmPackageDependency SuperTest = new("supertest", "^6.2.4");
    public static NpmPackageDependency SwaggerUiExpress = new("swagger-ui-express", "^4.1.6");
    public static NpmPackageDependency TsconfigPaths = new("tsconfig-paths", "^4.0.0");
    public static NpmPackageDependency TsJest = new("ts-jest", "^28.0.0");
    public static NpmPackageDependency TsLoader = new("ts-loader", "^9.3.1");
    public static NpmPackageDependency TsNode = new("ts-node", "^10.8.2");
    public static NpmPackageDependency Typescript = new("typescript", "^4.7.4");
}