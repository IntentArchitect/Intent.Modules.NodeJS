import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TpcFkDerivedClass } from './../domain/entities/TPC/InheritanceAssociations/tpc-fk-derived-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TpcFkDerivedClass)
export class TpcFkDerivedClassRepository extends Repository<TpcFkDerivedClass> {
}