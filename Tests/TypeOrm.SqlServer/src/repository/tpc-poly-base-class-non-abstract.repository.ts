import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_BaseClassNonAbstract } from './../domain/entities/TPC/Polymorphic/tpc-poly-base-class-non-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_BaseClassNonAbstract)
export class TpcPoly_BaseClassNonAbstractRepository extends Repository<TpcPoly_BaseClassNonAbstract> {
}