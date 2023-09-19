import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_TopLevel } from './../domain/entities/TPT/Polymorphic/tpt-poly-top-level.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_TopLevel)
export class TptPoly_TopLevelRepository extends Repository<TptPoly_TopLevel> {
}