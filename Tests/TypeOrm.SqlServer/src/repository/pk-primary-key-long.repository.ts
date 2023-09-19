import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { PK_PrimaryKeyLong } from './../domain/entities/ExplicitKeys/pk-primary-key-long.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(PK_PrimaryKeyLong)
export class PK_PrimaryKeyLongRepository extends Repository<PK_PrimaryKeyLong> {
}