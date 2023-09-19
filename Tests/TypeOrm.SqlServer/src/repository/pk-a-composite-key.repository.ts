import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { PK_A_CompositeKey } from './../domain/entities/ExplicitKeys/pk-a-composite-key.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(PK_A_CompositeKey)
export class PK_A_CompositeKeyRepository extends Repository<PK_A_CompositeKey> {
}