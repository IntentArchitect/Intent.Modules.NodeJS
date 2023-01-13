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
        public const string PartitionKey = "Dynamo DB Table Hash Key"; // TODO JL: Need to update element type name in modeler
        public const string SortKey = "Dynamo DB Table Range Key"; // TODO JL: Need to update element type name in modeler
        public const string SqsQueue = "SQS Queue";
        public const string Message = "Message";
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
    }

    public static class Role
    {
        public const string DynamoDbRepositories = "Infrastructure.Persistence.Repositories";
        public const string SqsSender = "Lib.SQS.Sender";
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
        /// </summary>
        public const string VariableName = "VariableName";

        /// <summary>
        /// Cast to a <see cref="IElement"/>.
        /// </summary>
        public const string SourceElement = "SourceElement";
    }
}