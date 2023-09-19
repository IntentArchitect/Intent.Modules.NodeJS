import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphConcreteBaseClassAssociated } from './../domain/entities/TPH/InheritanceAssociations/tph-concrete-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphConcreteBaseClassAssociated)
export class TphConcreteBaseClassAssociatedRepository extends Repository<TphConcreteBaseClassAssociated> {
}