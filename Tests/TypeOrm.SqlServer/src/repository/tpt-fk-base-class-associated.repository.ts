import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptFkBaseClassAssociated } from './../domain/entities/TPT/InheritanceAssociations/tpt-fk-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptFkBaseClassAssociated)
export class TptFkBaseClassAssociatedRepository extends Repository<TptFkBaseClassAssociated> {
}