import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_RootAbstract_Aggr } from './../domain/entities/TPT/Polymorphic/tpt-poly-root-abstract-aggr.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_RootAbstract_Aggr)
export class TptPoly_RootAbstract_AggrRepository extends Repository<TptPoly_RootAbstract_Aggr> {
}