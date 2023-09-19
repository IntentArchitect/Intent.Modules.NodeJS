import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptDerivedClassForConcreteAssociated } from './../domain/entities/TPT/InheritanceAssociations/tpt-derived-class-for-concrete-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptDerivedClassForConcreteAssociated)
export class TptDerivedClassForConcreteAssociatedRepository extends Repository<TptDerivedClassForConcreteAssociated> {
}