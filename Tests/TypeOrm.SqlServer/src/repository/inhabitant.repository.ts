import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { Inhabitant } from './../domain/entities/NestedAssociations/inhabitant.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(Inhabitant)
export class InhabitantRepository extends Repository<Inhabitant> {
}