import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { C_MultipleDependent } from './../domain/entities/Associations/c-multiple-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(C_MultipleDependent)
export class C_MultipleDependentRepository extends Repository<C_MultipleDependent> {
}