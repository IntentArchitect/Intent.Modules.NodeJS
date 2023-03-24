using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda;

internal class NpmPackageDependencies
{
    public static class Middy
    {
        public static NpmPackageDependency Core = new("@middy/core", "^4.0.9");
        /// <summary>
        /// Unless <see cref="TypeFest"/> is also installed with this, then the following <c>tsc</c> error occurs:
        /// <code>
        /// node_modules/@middy/http-json-body-parser/index.d.ts:3:27 - error TS2307: Cannot find module 'type-fest' or its corresponding type declarations.
        /// </code>
        /// </summary>
        public static NpmPackageDependency HttpJsonBodyParser = new("@middy/http-json-body-parser", "^4.0.9");
    }

    public static class Types
    {
        public static NpmPackageDependency AwsLambda = new("@types/aws-lambda", "^8.10.109", true);
    }

    /// <summary>
    /// Required by <see cref="Middy.HttpJsonBodyParser"/>.
    /// </summary>
    public static NpmPackageDependency TypeFest = new("type-fest", "^3.5.1", true);
    public static NpmPackageDependency AwsLambda = new("aws-lambda", "^1.0.7", true);
}