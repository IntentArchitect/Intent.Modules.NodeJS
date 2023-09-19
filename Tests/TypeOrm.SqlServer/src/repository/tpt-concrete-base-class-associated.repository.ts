import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptConcreteBaseClassAssociated } from './../domain/entities/TPT/InheritanceAssociations/tpt-concrete-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptConcreteBaseClassAssociated)
export class TptConcreteBaseClassAssociatedRepository extends Repository<TptConcreteBaseClassAssociated> {
}