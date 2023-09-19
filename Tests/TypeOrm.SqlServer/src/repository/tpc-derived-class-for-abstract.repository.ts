import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcDerivedClassForAbstract } from './../domain/entities/TPC/InheritanceAssociations/tpc-derived-class-for-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcDerivedClassForAbstract)
export class TpcDerivedClassForAbstractRepository extends Repository<TpcDerivedClassForAbstract> {
}