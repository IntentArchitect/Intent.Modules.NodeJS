import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { FK_A_CompositeForeignKey } from './../domain/entities/ExplicitKeys/fk-a-composite-foreign-key.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(FK_A_CompositeForeignKey)
export class FK_A_CompositeForeignKeyRepository extends Repository<FK_A_CompositeForeignKey> {
}