import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { DefaultIndex } from './../domain/entities/Indexes/default-index.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(DefaultIndex)
export class DefaultIndexRepository extends Repository<DefaultIndex> {
}