import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { L_SelfReferenceMultiple } from './../domain/entities/Associations/l-self-reference-multiple.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(L_SelfReferenceMultiple)
export class L_SelfReferenceMultipleRepository extends Repository<L_SelfReferenceMultiple> {
}