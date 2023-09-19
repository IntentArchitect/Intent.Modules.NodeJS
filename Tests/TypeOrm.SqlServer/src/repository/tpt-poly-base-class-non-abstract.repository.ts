import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_BaseClassNonAbstract } from './../domain/entities/TPT/Polymorphic/tpt-poly-base-class-non-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_BaseClassNonAbstract)
export class TptPoly_BaseClassNonAbstractRepository extends Repository<TptPoly_BaseClassNonAbstract> {
}