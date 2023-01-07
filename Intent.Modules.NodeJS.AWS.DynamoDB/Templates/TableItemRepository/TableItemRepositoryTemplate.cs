using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.TableItemRepository
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class TableItemRepositoryTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} extends {this.GetTableItemRepositoryBaseName()}<{this.GetTableItemName()}, {Model.GetTable().GetKey()}> {{
}}
";
        }
    }
}