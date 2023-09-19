import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphAbstractBaseClass } from './../domain/entities/TPH/InheritanceAssociations/tph-abstract-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphAbstractBaseClass)
export class TphAbstractBaseClassRepository extends Repository<TphAbstractBaseClass> {
}