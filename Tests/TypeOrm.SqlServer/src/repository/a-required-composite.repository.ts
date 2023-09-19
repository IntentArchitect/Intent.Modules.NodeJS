import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { A_RequiredComposite } from './../domain/entities/Associations/a-required-composite.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(A_RequiredComposite)
export class A_RequiredCompositeRepository extends Repository<A_RequiredComposite> {
}