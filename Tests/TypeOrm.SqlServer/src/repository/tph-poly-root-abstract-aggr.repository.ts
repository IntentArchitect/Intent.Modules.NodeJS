import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_RootAbstract_Aggr } from './../domain/entities/TPH/Polymorphic/tph-poly-root-abstract-aggr.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_RootAbstract_Aggr)
export class TphPoly_RootAbstract_AggrRepository extends Repository<TphPoly_RootAbstract_Aggr> {
}