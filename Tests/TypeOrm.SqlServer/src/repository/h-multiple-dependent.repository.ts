import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { H_MultipleDependent } from './../domain/entities/Associations/h-multiple-dependent.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(H_MultipleDependent)
export class H_MultipleDependentRepository extends Repository<H_MultipleDependent> {
}