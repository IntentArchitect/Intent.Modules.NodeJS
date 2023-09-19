import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcFkBaseClass } from './../domain/entities/TPC/InheritanceAssociations/tpc-fk-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcFkBaseClass)
export class TpcFkBaseClassRepository extends Repository<TpcFkBaseClass> {
}