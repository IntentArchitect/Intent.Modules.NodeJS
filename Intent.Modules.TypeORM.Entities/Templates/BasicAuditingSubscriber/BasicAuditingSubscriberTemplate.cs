using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.BasicAuditingSubscriber
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class BasicAuditingSubscriberTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ Inject }} from '@nestjs/common';
import {{ DataSource, EntitySubscriberInterface, EventSubscriber, InsertEvent, UpdateEvent }} from 'typeorm';
import {{ CLS_REQ }} from 'nestjs-cls'

@EventSubscriber()
export class {ClassName} implements EntitySubscriberInterface {{
    constructor(
        @Inject(CLS_REQ) private readonly request: Request,
        dataSource: DataSource,
    ) {{
        dataSource.subscribers.push(this);
    }}

    beforeInsert(event: InsertEvent<any>): void {{
        if (event.metadata.propertiesMap[""createdDate""]) {{
            event.entity.createdDate = new Date().toISOString();
        }}

        const username = (this.request as any).user?.username as string;
        if (event.metadata.propertiesMap[""createdBy""]) {{
            event.entity.createdBy = username ?? ""(anonymous)"";
        }}
    }}

    beforeUpdate(event: UpdateEvent<any>): void {{
        if (event.metadata.propertiesMap[""lastModifiedDate""]) {{
            event.entity.lastModifiedDate = new Date().toISOString();
        }}

        const username = (this.request as any).user?.username as string;
        if (event.metadata.propertiesMap[""lastModifiedBy""]) {{
            event.entity.lastModifiedBy = username ?? ""(anonymous)"";
        }}
    }}
}}";
        }
    }
}