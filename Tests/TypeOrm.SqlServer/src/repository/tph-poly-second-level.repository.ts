import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_SecondLevel } from './../domain/entities/TPH/Polymorphic/tph-poly-second-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_SecondLevel)
export class TphPoly_SecondLevelRepository extends Repository<TphPoly_SecondLevel> {
}