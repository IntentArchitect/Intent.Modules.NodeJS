import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptFkBaseClass } from './../domain/entities/TPT/InheritanceAssociations/tpt-fk-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptFkBaseClass)
export class TptFkBaseClassRepository extends Repository<TptFkBaseClass> {
}