import { Repository } from 'typeorm';
import { CustomRepository } from './../typeorm/typeorm-ex.decorator';
import { Texture } from './../domain/entities/NestedAssociations/texture.entity';
import { IntentMerge } from './../intent/intent.decorators';

@IntentMerge()
@CustomRepository(Texture)
export class TextureRepository extends Repository<Texture> {
}