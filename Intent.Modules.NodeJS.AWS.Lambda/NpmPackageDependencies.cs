using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda
{
    internal class NpmPackageDependencies
    {
        public static class MiddyPackages
        {
            public static NpmPackageDependency Core = new NpmPackageDependency("@middy/core", "^3.4.0");
            public static NpmPackageDependency HttpJsonBodyParser = new NpmPackageDependency("@middy/http-json-body-parser", "^3.4.0");
        }

        public static class Types
        {
            public static NpmPackageDependency AwsLambda = new NpmPackageDependency("@types/aws-lambda", "^8.10.109", true);
        }

        public static NpmPackageDependency AwsLambda = new NpmPackageDependency("aws-lambda", "^1.0.7", true);
        public static NpmPackageDependency JsonSchemaToTs = new NpmPackageDependency("json-schema-to-ts", "^2.6.2", true);
    }
}
