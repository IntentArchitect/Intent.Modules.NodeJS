import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptDerivedClassForAbstractAssociated } from './../domain/entities/TPT/InheritanceAssociations/tpt-derived-class-for-abstract-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptDerivedClassForAbstractAssociated)
export class TptDerivedClassForAbstractAssociatedRepository extends Repository<TptDerivedClassForAbstractAssociated> {
}