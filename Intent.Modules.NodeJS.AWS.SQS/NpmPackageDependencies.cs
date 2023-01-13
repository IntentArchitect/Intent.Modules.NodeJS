using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.SQS;

internal static class NpmPackageDependencies
{
    public static class AwsSdk
    {
        public static NpmPackageDependency ClientSqs = new("@aws-sdk/client-sqs", "^3.245.0", isDevDependency: true);
    }
}