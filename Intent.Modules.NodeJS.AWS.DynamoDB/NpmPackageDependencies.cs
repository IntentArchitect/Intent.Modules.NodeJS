using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.DynamoDB;

internal static class NpmPackageDependencies
{
    public static class AwsSdk
    {
        public static readonly NpmPackageDependency ClientDynamodb = new("@aws-sdk/client-dynamodb", "^3.213.0");
        public static readonly NpmPackageDependency LibDynamodb = new("@aws-sdk/lib-dynamodb", "^3.213.0");
    }
}