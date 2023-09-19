import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_ConcreteA } from './../domain/entities/TPT/Polymorphic/tpt-poly-concrete-a.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_ConcreteA)
export class TptPoly_ConcreteARepository extends Repository<TptPoly_ConcreteA> {
}