import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_ConcreteB } from './../domain/entities/TPT/Polymorphic/tpt-poly-concrete-b.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_ConcreteB)
export class TptPoly_ConcreteBRepository extends Repository<TptPoly_ConcreteB> {
}