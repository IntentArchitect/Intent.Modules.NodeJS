import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptFkDerivedClass } from './../domain/entities/TPT/InheritanceAssociations/tpt-fk-derived-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptFkDerivedClass)
export class TptFkDerivedClassRepository extends Repository<TptFkDerivedClass> {
}