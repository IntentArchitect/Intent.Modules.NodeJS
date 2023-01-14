using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.S3;

internal static class NpmPackageDependencies
{
    public static class AwsSdk
    {
        public static NpmPackageDependency ClientS3 = new("@aws-sdk/client-s3", "^3.245.0", isDevDependency: true);
    }
}