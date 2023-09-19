import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_TopLevel } from './../domain/entities/TPC/Polymorphic/tpc-poly-top-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_TopLevel)
export class TpcPoly_TopLevelRepository extends Repository<TpcPoly_TopLevel> {
}