import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphDerivedClassForAbstract } from './../domain/entities/TPH/InheritanceAssociations/tph-derived-class-for-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphDerivedClassForAbstract)
export class TphDerivedClassForAbstractRepository extends Repository<TphDerivedClassForAbstract> {
}