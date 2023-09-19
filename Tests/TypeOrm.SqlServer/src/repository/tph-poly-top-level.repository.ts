import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphPoly_TopLevel } from './../domain/entities/TPH/Polymorphic/tph-poly-top-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphPoly_TopLevel)
export class TphPoly_TopLevelRepository extends Repository<TphPoly_TopLevel> {
}