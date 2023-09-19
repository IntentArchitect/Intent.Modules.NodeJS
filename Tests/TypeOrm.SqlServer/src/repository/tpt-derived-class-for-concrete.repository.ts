import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TptDerivedClassForConcrete } from './../domain/entities/TPT/InheritanceAssociations/tpt-derived-class-for-concrete.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TptDerivedClassForConcrete)
export class TptDerivedClassForConcreteRepository extends Repository<TptDerivedClassForConcrete> {
}