import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { PK_PrimaryKeyInt } from './../domain/entities/ExplicitKeys/pk-primary-key-int.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(PK_PrimaryKeyInt)
export class PK_PrimaryKeyIntRepository extends Repository<PK_PrimaryKeyInt> {
}