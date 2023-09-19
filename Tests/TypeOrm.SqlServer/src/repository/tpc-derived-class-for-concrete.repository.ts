import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcDerivedClassForConcrete } from './../domain/entities/TPC/InheritanceAssociations/tpc-derived-class-for-concrete.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcDerivedClassForConcrete)
export class TpcDerivedClassForConcreteRepository extends Repository<TpcDerivedClassForConcrete> {
}