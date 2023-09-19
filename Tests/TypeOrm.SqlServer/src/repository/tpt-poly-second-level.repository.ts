import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_SecondLevel } from './../domain/entities/TPT/Polymorphic/tpt-poly-second-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_SecondLevel)
export class TptPoly_SecondLevelRepository extends Repository<TptPoly_SecondLevel> {
}