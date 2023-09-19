import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { StereotypeIndex } from './../domain/entities/Indexes/stereotype-index.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(StereotypeIndex)
export class StereotypeIndexRepository extends Repository<StereotypeIndex> {
}