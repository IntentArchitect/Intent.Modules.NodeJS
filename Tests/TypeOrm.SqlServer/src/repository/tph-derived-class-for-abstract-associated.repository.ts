import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphDerivedClassForAbstractAssociated } from './../domain/entities/TPH/InheritanceAssociations/tph-derived-class-for-abstract-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphDerivedClassForAbstractAssociated)
export class TphDerivedClassForAbstractAssociatedRepository extends Repository<TphDerivedClassForAbstractAssociated> {
}