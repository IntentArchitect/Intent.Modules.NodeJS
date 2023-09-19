import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphFkDerivedClass } from './../domain/entities/TPH/InheritanceAssociations/tph-fk-derived-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphFkDerivedClass)
export class TphFkDerivedClassRepository extends Repository<TphFkDerivedClass> {
}