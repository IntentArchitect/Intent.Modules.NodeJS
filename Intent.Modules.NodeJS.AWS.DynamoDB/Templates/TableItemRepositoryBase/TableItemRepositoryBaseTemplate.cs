using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepositoryBase
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class TableItemRepositoryBaseTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ DynamoDBDocumentClient, PutCommand, GetCommand, ScanCommand, DeleteCommand }} from ""@aws-sdk/lib-dynamodb"";

export abstract class {ClassName}<TItem, TKey> {{
    constructor(
        protected client: DynamoDBDocumentClient,
        protected tableName: string
    ) {{ }}

    async put(item: TItem): Promise<void> {{
        const command = new PutCommand({{
            Item: item as any,
            TableName: this.tableName
        }});

        await this.client.send(command);
    }}

    async get(key: TKey): Promise<TItem> {{
        const command = new GetCommand({{
            Key: key as any,
            TableName: this.tableName
        }});

        var response = await this.client.send(command);

        return response.Item as any;
    }}

    async scan(): Promise<TItem[]> {{
        const command = new ScanCommand({{
            TableName: this.tableName
        }});

        var response = await this.client.send(command);

        return response.Items as any;
    }}

    async delete(key: TKey): Promise<void> {{
        const command = new DeleteCommand({{
            Key: key as any,
            TableName: this.tableName
        }});

        await this.client.send(command);
    }}
}}
";
        }
    }
}