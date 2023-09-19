import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphDerivedClassForConcreteAssociated } from './../domain/entities/TPH/InheritanceAssociations/tph-derived-class-for-concrete-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphDerivedClassForConcreteAssociated)
export class TphDerivedClassForConcreteAssociatedRepository extends Repository<TphDerivedClassForConcreteAssociated> {
}