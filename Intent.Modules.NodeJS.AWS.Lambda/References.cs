﻿namespace Intent.Modules.NodeJS.AWS.Lambda;

internal static class References
{
    public static class Elements
    {
        public const string ApiGatewayEndpoint = "API Gateway Endpoint";
        public const string DynamoDbTable = "DynamoDB Table";
    }

    public static class Roles
    {
        public const string DynamoDbRepositories = "Infrastructure.Persistence.Repositories";
    }
}