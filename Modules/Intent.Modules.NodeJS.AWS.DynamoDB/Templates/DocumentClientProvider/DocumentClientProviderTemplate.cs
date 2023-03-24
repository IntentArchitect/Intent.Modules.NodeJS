using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.DocumentClientProvider
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class DocumentClientProviderTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ DynamoDBClient }} from ""@aws-sdk/client-dynamodb"";
import {{ DynamoDBDocumentClient }} from ""@aws-sdk/lib-dynamodb"";

export class {ClassName} {{
    static client: DynamoDBDocumentClient;

    static get(): DynamoDBDocumentClient {{
        if ({ClassName}.client == null) {{
            {ClassName}.client = DynamoDBDocumentClient.from(new DynamoDBClient({{
                logger: {{ }} as any // Workaround: https://github.com/aws/aws-sdk-js-v3/issues/3846#issuecomment-1319141254
            }}), {{
                marshallOptions: {{
                    removeUndefinedValues: true
                }}
            }});
        }}

        return {ClassName}.client;
    }}
}}
";
        }

        private IEnumerable<string> GetMembers()
        {
            var members = new List<string>();

            // example: adding a constructor
            members.Add($@"
    constructor() {{
    }}");
            return members;
        }
    }
}