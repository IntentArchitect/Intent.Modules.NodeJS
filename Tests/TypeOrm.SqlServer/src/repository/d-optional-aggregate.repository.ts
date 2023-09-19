import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { D_OptionalAggregate } from './../domain/entities/Associations/d-optional-aggregate.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(D_OptionalAggregate)
export class D_OptionalAggregateRepository extends Repository<D_OptionalAggregate> {
}