import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcDerivedClassForAbstractAssociated } from './../domain/entities/TPC/InheritanceAssociations/tpc-derived-class-for-abstract-associated.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcDerivedClassForAbstractAssociated)
export class TpcDerivedClassForAbstractAssociatedRepository extends Repository<TpcDerivedClassForAbstractAssociated> {
}