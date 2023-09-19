import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcFkAssociatedClass } from './../domain/entities/TPC/InheritanceAssociations/tpc-fk-associated-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcFkAssociatedClass)
export class TpcFkAssociatedClassRepository extends Repository<TpcFkAssociatedClass> {
}