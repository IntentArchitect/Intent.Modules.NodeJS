import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptFkAssociatedClass } from './../domain/entities/TPT/InheritanceAssociations/tpt-fk-associated-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptFkAssociatedClass)
export class TptFkAssociatedClassRepository extends Repository<TptFkAssociatedClass> {
}