import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { B_OptionalAggregate } from './../domain/entities/Associations/b-optional-aggregate.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(B_OptionalAggregate)
export class B_OptionalAggregateRepository extends Repository<B_OptionalAggregate> {
}