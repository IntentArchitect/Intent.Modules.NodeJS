import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_BaseClassNonAbstract } from './../domain/entities/TPH/Polymorphic/tph-poly-base-class-non-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_BaseClassNonAbstract)
export class TphPoly_BaseClassNonAbstractRepository extends Repository<TphPoly_BaseClassNonAbstract> {
}