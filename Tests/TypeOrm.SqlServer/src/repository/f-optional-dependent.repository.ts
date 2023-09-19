import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { F_OptionalDependent } from './../domain/entities/Associations/f-optional-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(F_OptionalDependent)
export class F_OptionalDependentRepository extends Repository<F_OptionalDependent> {
}