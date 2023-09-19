import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcPoly_SecondLevel } from './../domain/entities/TPC/Polymorphic/tpc-poly-second-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcPoly_SecondLevel)
export class TpcPoly_SecondLevelRepository extends Repository<TpcPoly_SecondLevel> {
}