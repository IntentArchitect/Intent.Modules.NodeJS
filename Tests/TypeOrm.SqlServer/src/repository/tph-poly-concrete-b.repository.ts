import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_ConcreteB } from './../domain/entities/TPH/Polymorphic/tph-poly-concrete-b.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_ConcreteB)
export class TphPoly_ConcreteBRepository extends Repository<TphPoly_ConcreteB> {
}