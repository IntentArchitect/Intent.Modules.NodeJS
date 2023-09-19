import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphFkAssociatedClass } from './../domain/entities/TPH/InheritanceAssociations/tph-fk-associated-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphFkAssociatedClass)
export class TphFkAssociatedClassRepository extends Repository<TphFkAssociatedClass> {
}