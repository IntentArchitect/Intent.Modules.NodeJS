import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { TestEntity } from './../domain/entities/test-entity.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(TestEntity)
export class TestEntityRepository extends Repository<TestEntity> {
}