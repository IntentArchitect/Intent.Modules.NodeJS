import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphFkBaseClass } from './../domain/entities/TPH/InheritanceAssociations/tph-fk-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphFkBaseClass)
export class TphFkBaseClassRepository extends Repository<TphFkBaseClass> {
}