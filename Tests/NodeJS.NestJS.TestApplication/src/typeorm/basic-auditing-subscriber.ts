import { Inject } from '@nestjs/common';
import { Request } from 'express';
import { CLS_REQ } from 'nestjs-cls'
import { DataSource, EntitySubscriberInterface, EventSubscriber, InsertEvent, UpdateEvent } from 'typeorm';

@EventSubscriber()
export class BasicAuditingSubscriber implements EntitySubscriberInterface {
    constructor(
        @Inject(CLS_REQ) private readonly request: Request,
        dataSource: DataSource,
    ) {
        dataSource.subscribers.push(this);
    }

    beforeInsert(event: InsertEvent<any>): void {
        if (event.metadata.propertiesMap["createdDate"]) {
            event.entity.createdDate = new Date().toISOString();
        }

        const username = (this.request as any).user?.username as string;
        if (event.metadata.propertiesMap["createdBy"]) {
            event.entity.createdBy = username ?? "(anonymous)";
        }
    }

    beforeUpdate(event: UpdateEvent<any>): void {
        if (event.metadata.propertiesMap["lastModifiedDate"]) {
            event.entity.lastModifiedDate = new Date().toISOString();
        }

        const username = (this.request as any).user?.username as string;
        if (event.metadata.propertiesMap["lastModifiedBy"]) {
            event.entity.lastModifiedBy = username ?? "(anonymous)";
        }
    }
}