using System;
using System.Collections.Generic;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepositories
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class EntityRepositoriesTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} {{
    private static readonly tableName = {GetTableName()};{string.Concat(GetMembers())}
}}
";
        }

        private IEnumerable<string> GetMembers()
        {
            foreach (var entity in Model.Entities)
            {
                yield return @$"
    private static _{Name(entity)}: {Repository(entity)};";
            }

            foreach (var entity in Model.Entities)
            {
                yield return $@"

    get {entity.Name.ToCamelCase()}() : {Repository(entity)} {{
        if ({ClassName}._{Name(entity)} == null) {{
            {ClassName}._{Name(entity)} = new {Repository(entity)}({this.GetDocumentClientProviderName()}.get(), {ClassName}.tableName);
        }}

        return {ClassName}._{Name(entity)};
    }}";
            }

            static string Name(EntityModel entity) => entity.Name.ToCamelCase();
            string Repository(EntityModel entity) => this.GetEntityRepositoryName(entity);
        }

        private string GetTableName()
        {
            if (!TryGetTemplate<ITypescriptFileBuilderTemplate>(Constants.Role.Stacks, Model.InternalElement.Package.Id, out var template) ||
                !template.TypescriptFile.Classes[0].Constructors[0]
                    .TryGetMetadata(Constants.MetadataKey.DynamoDbTableName, out var dynamoDbTableName))
            {
                return $"'{Model.Name}'";
            }

            return $"process.env.{dynamoDbTableName}";
        }
    }
}