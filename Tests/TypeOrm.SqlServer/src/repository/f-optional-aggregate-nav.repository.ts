import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { F_OptionalAggregateNav } from './../domain/entities/Associations/f-optional-aggregate-nav.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(F_OptionalAggregateNav)
export class F_OptionalAggregateNavRepository extends Repository<F_OptionalAggregateNav> {
}