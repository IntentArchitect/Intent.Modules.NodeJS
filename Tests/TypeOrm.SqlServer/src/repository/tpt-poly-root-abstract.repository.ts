import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptPoly_RootAbstract } from './../domain/entities/TPT/Polymorphic/tpt-poly-root-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptPoly_RootAbstract)
export class TptPoly_RootAbstractRepository extends Repository<TptPoly_RootAbstract> {
}