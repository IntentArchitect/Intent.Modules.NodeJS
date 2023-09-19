import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { G_RequiredCompositeNav } from './../domain/entities/Associations/g-required-composite-nav.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(G_RequiredCompositeNav)
export class G_RequiredCompositeNavRepository extends Repository<G_RequiredCompositeNav> {
}