import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptAbstractBaseClass } from './../domain/entities/TPT/InheritanceAssociations/tpt-abstract-base-class.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptAbstractBaseClass)
export class TptAbstractBaseClassRepository extends Repository<TptAbstractBaseClass> {
}