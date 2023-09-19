import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { E_RequiredCompositeNav } from './../domain/entities/Associations/e-required-composite-nav.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(E_RequiredCompositeNav)
export class E_RequiredCompositeNavRepository extends Repository<E_RequiredCompositeNav> {
}