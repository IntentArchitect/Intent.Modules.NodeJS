import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_ConcreteA } from './../domain/entities/TPH/Polymorphic/tph-poly-concrete-a.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_ConcreteA)
export class TphPoly_ConcreteARepository extends Repository<TphPoly_ConcreteA> {
}