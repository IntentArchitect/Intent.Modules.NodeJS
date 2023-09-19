import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { ComplexDefaultIndex } from './../domain/entities/Indexes/complex-default-index.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(ComplexDefaultIndex)
export class ComplexDefaultIndexRepository extends Repository<ComplexDefaultIndex> {
}