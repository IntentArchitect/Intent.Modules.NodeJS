import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { B_OptionalDependent } from './../domain/entities/Associations/b-optional-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(B_OptionalDependent)
export class B_OptionalDependentRepository extends Repository<B_OptionalDependent> {
}