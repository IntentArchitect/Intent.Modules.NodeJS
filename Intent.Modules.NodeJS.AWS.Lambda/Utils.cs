using Intent.Metadata.Models;
using Intent.Modules.NodeJS.AWS.CDK;

namespace Intent.Modules.NodeJS.AWS.Lambda;

internal static class Utils
{
    public static bool IsApiGateway(this ICanBeReferencedType canBeReferencedType)
    {
        return canBeReferencedType is IElement { SpecializationType: Constants.ElementName.ApiGatewayEndpoint };
    }

    public static bool IsDynamoDb(this ICanBeReferencedType canBeReferencedType)
    {
        return canBeReferencedType is IElement { SpecializationType: Constants.ElementName.DynamoDbTable };
    }
}