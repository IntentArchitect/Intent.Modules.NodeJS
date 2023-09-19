import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcFkBaseClassAssociated } from './../domain/entities/TPC/InheritanceAssociations/tpc-fk-base-class-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcFkBaseClassAssociated)
export class TpcFkBaseClassAssociatedRepository extends Repository<TpcFkBaseClassAssociated> {
}