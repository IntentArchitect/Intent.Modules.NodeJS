import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { ClassA } from './../domain/entities/class-a.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(ClassA)
export class ClassARepository extends Repository<ClassA> {
}