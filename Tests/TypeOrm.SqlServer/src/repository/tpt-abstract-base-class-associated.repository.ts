import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptAbstractBaseClassAssociated } from './../domain/entities/TPT/InheritanceAssociations/tpt-abstract-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptAbstractBaseClassAssociated)
export class TptAbstractBaseClassAssociatedRepository extends Repository<TptAbstractBaseClassAssociated> {
}