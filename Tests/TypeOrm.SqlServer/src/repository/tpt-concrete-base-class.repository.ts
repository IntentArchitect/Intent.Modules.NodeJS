import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptConcreteBaseClass } from './../domain/entities/TPT/InheritanceAssociations/tpt-concrete-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptConcreteBaseClass)
export class TptConcreteBaseClassRepository extends Repository<TptConcreteBaseClass> {
}