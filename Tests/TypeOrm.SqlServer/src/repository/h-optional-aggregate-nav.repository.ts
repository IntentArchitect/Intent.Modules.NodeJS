import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { H_OptionalAggregateNav } from './../domain/entities/Associations/h-optional-aggregate-nav.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(H_OptionalAggregateNav)
export class H_OptionalAggregateNavRepository extends Repository<H_OptionalAggregateNav> {
}