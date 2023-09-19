import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { Tree } from './../domain/entities/NestedAssociations/tree.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(Tree)
export class TreeRepository extends Repository<Tree> {
}