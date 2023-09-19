import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_RootAbstract } from './../domain/entities/TPH/Polymorphic/tph-poly-root-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_RootAbstract)
export class TphPoly_RootAbstractRepository extends Repository<TphPoly_RootAbstract> {
}