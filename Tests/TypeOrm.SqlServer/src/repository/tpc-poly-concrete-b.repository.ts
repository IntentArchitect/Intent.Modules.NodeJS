import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_ConcreteB } from './../domain/entities/TPC/Polymorphic/tpc-poly-concrete-b.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_ConcreteB)
export class TpcPoly_ConcreteBRepository extends Repository<TpcPoly_ConcreteB> {
}