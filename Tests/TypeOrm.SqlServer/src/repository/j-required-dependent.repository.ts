import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { J_RequiredDependent } from './../domain/entities/Associations/j-required-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(J_RequiredDependent)
export class J_RequiredDependentRepository extends Repository<J_RequiredDependent> {
}