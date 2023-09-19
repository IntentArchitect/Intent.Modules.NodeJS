import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphFkBaseClassAssociated } from './../domain/entities/TPH/InheritanceAssociations/tph-fk-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphFkBaseClassAssociated)
export class TphFkBaseClassAssociatedRepository extends Repository<TphFkBaseClassAssociated> {
}