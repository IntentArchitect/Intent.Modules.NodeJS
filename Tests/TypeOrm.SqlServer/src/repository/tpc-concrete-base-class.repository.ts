import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcConcreteBaseClass } from './../domain/entities/TPC/InheritanceAssociations/tpc-concrete-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcConcreteBaseClass)
export class TpcConcreteBaseClassRepository extends Repository<TpcConcreteBaseClass> {
}