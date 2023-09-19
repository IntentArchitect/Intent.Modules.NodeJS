import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphConcreteBaseClass } from './../domain/entities/TPH/InheritanceAssociations/tph-concrete-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphConcreteBaseClass)
export class TphConcreteBaseClassRepository extends Repository<TphConcreteBaseClass> {
}