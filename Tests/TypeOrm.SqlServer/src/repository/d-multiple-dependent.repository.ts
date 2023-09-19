import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { D_MultipleDependent } from './../domain/entities/Associations/d-multiple-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(D_MultipleDependent)
export class D_MultipleDependentRepository extends Repository<D_MultipleDependent> {
}