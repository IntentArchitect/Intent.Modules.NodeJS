using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.Datasource
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class DatasourceTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ DataSource }} from 'typeorm';

export default new DataSource({this.GetOrmConfigName()} as any);";
        }
    }
}