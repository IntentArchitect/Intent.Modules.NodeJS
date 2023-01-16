using System.Collections.Generic;
using Intent.Metadata.Models;

namespace Intent.Modules.NodeJS.AWS.CDK;

internal static class Constants
{
    public static class ElementName
    {
        public const string ApiGatewayEndpoint = "API Gateway Endpoint";
        public const string DynamoDbTable = "DynamoDB Table";
        public const string IamPolicyStatement = "IAM Policy Statement";
        public const string LambdaFunction = "Lambda Function";
        public const string PartitionKey = "Partition Key";
        public const string SortKey = "Sort Key";
        public const string SqsQueue = "SQS Queue";
        public const string Message = "Message";
        public const string Parameter = "Parameter";
        public const string S3Bucket = "S3 Bucket";
        public const string GraphQLSchemaType = "GraphQL Schema Type";
        public const string GraphQLMutation = "GraphQL Mutation";
        public const string GraphQLEndpoint = "GraphQL Endpoint";
        public const string GraphQLSchemaField = "GraphQL Schema Field";
        public const string GraphQLParameter = "GraphQL Parameter";
        public const string Entity = "Entity";
    }

    public static class Stereotype
    {
        public static class DynamoDbTableSettings
        {
            public const string Name = "DynamoDB Table Settings";

            public static class Property
            {
                public const string RemovalPolicy = "Removal Policy";
            }
        }

        public static class S3BucketSettings
        {
            public const string Name = "S3 Bucket Settings";

            public static class Property
            {
                public const string RemovalPolicy = "Removal Policy";
                public const string Versioned = "Versioned";
            }
        }
    }

    public static class Role
    {
        public const string DynamoDbRepositories = "Services.Repositories";
        public const string SqsSender = "Lib.SQS.Sender";
        public const string Stacks = "Services.Stack";
        public const string S3BucketClient = "Lib.S3.BucketClient";
        public const string LambdaHandler = "Services.Functions.Handler";
        public const string GraphQlSchema = "Services.AppSync.GraphQlSchema";
    }

    public static class MetadataKey
    {
        /// <summary>
        /// Cast to a <see cref="Dictionary{TKey,TValue}"/> where <c>TKey</c> is <see cref="string"/>
        /// and <c>TValue</c> is <see cref="string"/>.
        /// </summary>
        public const string EnvironmentVariables = "EnvironmentVariables";

        /// <summary>
        /// Cast to a <see cref="string"/>.
        ///
        /// Contains the name of the environment variable which holds the table name expression.
        /// </summary>
        public const string DynamoDbTableName = "DynamoDbTableName";

        /// <summary>
        /// Cast to a <see cref="string"/>.
        ///
        /// Contains the name of the environment variable which holds the queue URL expression.
        /// </summary>
        public const string SqsQueueUrl = "SqsQueueUrl";

        /// <summary>
        /// Cast to a <see cref="string"/>.
        ///
        /// Contains the name of the environment variable which holds the bucket name expression.
        /// </summary>
        public const string S3BucketName = "S3BucketName";

        /// <summary>
        /// Cast to a <see cref="string"/>.
        /// </summary>
        public const string VariableName = "VariableName";

        /// <summary>
        /// Cast to a <see cref="IElement"/>.
        /// </summary>
        public const string SourceElement = "SourceElement";
    }
}