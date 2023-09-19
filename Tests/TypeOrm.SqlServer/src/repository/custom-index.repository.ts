import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { CustomIndex } from './../domain/entities/Indexes/custom-index.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(CustomIndex)
export class CustomIndexRepository extends Repository<CustomIndex> {
}