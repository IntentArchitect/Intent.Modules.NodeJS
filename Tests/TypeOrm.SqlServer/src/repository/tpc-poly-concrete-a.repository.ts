import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_ConcreteA } from './../domain/entities/TPC/Polymorphic/tpc-poly-concrete-a.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_ConcreteA)
export class TpcPoly_ConcreteARepository extends Repository<TpcPoly_ConcreteA> {
}