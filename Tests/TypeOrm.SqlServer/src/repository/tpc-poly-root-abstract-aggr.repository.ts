import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_RootAbstract_Aggr } from './../domain/entities/TPC/Polymorphic/tpc-poly-root-abstract-aggr.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_RootAbstract_Aggr)
export class TpcPoly_RootAbstract_AggrRepository extends Repository<TpcPoly_RootAbstract_Aggr> {
}