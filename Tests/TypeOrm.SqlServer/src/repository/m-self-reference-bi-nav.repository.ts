import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { M_SelfReferenceBiNav } from './../domain/entities/Associations/m-self-reference-bi-nav.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(M_SelfReferenceBiNav)
export class M_SelfReferenceBiNavRepository extends Repository<M_SelfReferenceBiNav> {
}