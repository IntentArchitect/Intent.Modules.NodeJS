import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcConcreteBaseClassAssociated } from './../domain/entities/TPC/InheritanceAssociations/tpc-concrete-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcConcreteBaseClassAssociated)
export class TpcConcreteBaseClassAssociatedRepository extends Repository<TpcConcreteBaseClassAssociated> {
}