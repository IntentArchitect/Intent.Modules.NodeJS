import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcDerivedClassForConcreteAssociated } from './../domain/entities/TPC/InheritanceAssociations/tpc-derived-class-for-concrete-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcDerivedClassForConcreteAssociated)
export class TpcDerivedClassForConcreteAssociatedRepository extends Repository<TpcDerivedClassForConcreteAssociated> {
}