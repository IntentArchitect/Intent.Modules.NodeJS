import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TphDerivedClassForConcrete } from './../domain/entities/TPH/InheritanceAssociations/tph-derived-class-for-concrete.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TphDerivedClassForConcrete)
export class TphDerivedClassForConcreteRepository extends Repository<TphDerivedClassForConcrete> {
}