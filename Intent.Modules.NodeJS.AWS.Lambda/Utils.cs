using Intent.Metadata.Models;

namespace Intent.Modules.NodeJS.AWS.Lambda;

internal static class Utils
{
    public static bool IsApiGateway(this ICanBeReferencedType canBeReferencedType)
    {
        return canBeReferencedType is IElement { SpecializationType: References.Elements.ApiGatewayEndpoint };
    }

    public static bool IsDynamoDb(this ICanBeReferencedType canBeReferencedType)
    {
        return canBeReferencedType is IElement { SpecializationType: References.Elements.DynamoDbTable };
    }
}