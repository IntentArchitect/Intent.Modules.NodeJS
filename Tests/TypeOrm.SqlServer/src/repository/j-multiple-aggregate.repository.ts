import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { J_MultipleAggregate } from './../domain/entities/Associations/j-multiple-aggregate.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(J_MultipleAggregate)
export class J_MultipleAggregateRepository extends Repository<J_MultipleAggregate> {
}