using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepository
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class EntityRepositoryTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} extends {this.GetEntityRepositoryBaseName()}<{this.GetEntityName()}, {Model.GetTable().GetKey()}> {{
}}
";
        }
    }
}