import { Injectable } from '@nestjs/common';
import { ClassACreateDto } from './dto/ClassAS/class-a-create.dto';
import { ClassADto } from './dto/ClassAS/class-a.dto';
import { ClassAUpdateDto } from './dto/ClassAS/class-a-update.dto';
import { ClassARepository } from './../repository/class-a.repository';
import { ClassA } from './../domain/entities/class-a.entity';
import { IntentIgnoreBody } from './../intent/intent.decorators';

@Injectable()
export class ClassASService {

  //@IntentCanAdd()
  constructor(private classARepository: ClassARepository) {}

  @IntentIgnoreBody()
  async create(dto: ClassACreateDto): Promise<string> {
    const newClassA = {
      attribute: dto.attribute,
    } as ClassA;

    await this.classARepository.insert(newClassA);
    return newClassA.id;
  }

  @IntentIgnoreBody()
  async findById(id: string): Promise<ClassADto> {
    const classA = await this.classARepository.findOne({
      where: {
        id: id
      },
      relations: ClassADto.requiredRelations,
    });
    return ClassADto.fromClassA(classA);
  }

  @IntentIgnoreBody()
  async findAll(): Promise<ClassADto[]> {
    const classAs = await this.classARepository.find({
      relations: ClassADto.requiredRelations,
    });
    return classAs.map((x) => ClassADto.fromClassA(x));
  }

  @IntentIgnoreBody()
  async put(id: string, dto: ClassAUpdateDto): Promise<void> {
    throw new Error("Write your implementation for this service here...");
  }

  @IntentIgnoreBody()
  async delete(id: string): Promise<ClassADto> {
    const existingClassA = await this.classARepository.findOneBy({
      id: id,
    });
    await this.classARepository.remove(existingClassA);
  }
}
