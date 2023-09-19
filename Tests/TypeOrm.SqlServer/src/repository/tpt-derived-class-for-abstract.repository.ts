import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptDerivedClassForAbstract } from './../domain/entities/TPT/InheritanceAssociations/tpt-derived-class-for-abstract.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptDerivedClassForAbstract)
export class TptDerivedClassForAbstractRepository extends Repository<TptDerivedClassForAbstract> {
}