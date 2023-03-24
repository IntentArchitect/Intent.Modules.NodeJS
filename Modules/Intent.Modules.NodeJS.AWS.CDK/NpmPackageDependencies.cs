using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK;

internal static class NpmPackageDependencies
{
    public static class Types
    {
        public static NpmPackageDependency Node = new("@types/node", "18.11.9", isDevDependency: true);
    }

    public static NpmPackageDependency AwsCdk = new("aws-cdk", "^2.60.0", isDevDependency: true);
    public static NpmPackageDependency AwsCdkLib = new("aws-cdk-lib", "^2.60.0");
    public static NpmPackageDependency Esbuild = new("esbuild", "^0.17.0", true);
    public static NpmPackageDependency TsNode = new("ts-node", "^10.9.1", isDevDependency: true);
    public static NpmPackageDependency Typescript = new("typescript", "~4.9.3", isDevDependency: true);
}