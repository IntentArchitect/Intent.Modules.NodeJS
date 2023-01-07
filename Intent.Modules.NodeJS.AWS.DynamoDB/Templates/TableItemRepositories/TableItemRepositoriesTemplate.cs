using System;
using System.Collections.Generic;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepositories
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class TableItemRepositoriesTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} {{
    private static readonly tableName = ""{Model.Name}"";{string.Concat(GetMembers())}
}}
";
        }

        private IEnumerable<string> GetMembers()
        {
            foreach (var item in Model.Items)
            {
                yield return @$"
    private static _{Name(item)}: {Repository(item)};";
            }

            foreach (var item in Model.Items)
            {
                yield return $@"

    static get {item.Name.ToCamelCase()}() : {Repository(item)} {{
        if ({ClassName}._{Name(item)} == null) {{
            {ClassName}._{Name(item)} = new {Repository(item)}({this.GetDocumentClientProviderName()}.get(), {ClassName}.tableName);
        }}

        return {ClassName}._{Name(item)};
    }}";
            }

            string Name(DynamoDBItemModel item)
            {
                return item.Name.ToCamelCase();
            }

            string Repository(DynamoDBItemModel item)
            {
                return this.GetTableItemRepositoryName(item);
            }
        }
    }
}