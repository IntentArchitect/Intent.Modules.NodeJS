import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { K_SelfReference } from './../domain/entities/Associations/k-self-reference.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(K_SelfReference)
export class K_SelfReferenceRepository extends Repository<K_SelfReference> {
}