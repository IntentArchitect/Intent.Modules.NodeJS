import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphAbstractBaseClassAssociated } from './../domain/entities/TPH/InheritanceAssociations/tph-abstract-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphAbstractBaseClassAssociated)
export class TphAbstractBaseClassAssociatedRepository extends Repository<TphAbstractBaseClassAssociated> {
}