import { Injectable } from '@nestjs/common';
import { TestEntityCreateDto } from './dto/TestEntities/test-entity-create.dto';
import { TestEntityDto } from './dto/TestEntities/test-entity.dto';
import { TestEntityUpdateDto } from './dto/TestEntities/test-entity-update.dto';
import { TestEntityRepository } from './../repository/test-entity.repository';
import { TestEntity } from './../domain/entities/test-entity.entity';
import { IntentIgnoreBody } from './../intent/intent.decorators';
import { ApiResponse } from '@nestjs/swagger';

@Injectable()
export class TestEntitiesService {

  //@IntentCanAdd()
  constructor(private testEntityRepository: TestEntityRepository) {}

  @IntentIgnoreBody()
  async createTestEntity(dto: TestEntityCreateDto): Promise<string> {
    const newTestEntity = {
      id: undefined,
      binaryField: "",
      boolField: true,
      byteField: 1,
      charField: 1,
      dateField: new Date(),
      datetimeField: new Date(),
      datetimeoffsetField: new Date(),
      decimalField: 1,
      doubleField: 1,
      floatField: 1,
      guidField: "789bf853-16cb-412f-a192-3506a88eb890",
      intField: 1,
      longField: 1,
      shortField: 1,
      stringField: "string",
    } as TestEntity;

    await this.testEntityRepository.insert(newTestEntity);
    return newTestEntity.id;
  }

  @IntentIgnoreBody()
  async findTestEntityById(id: string): Promise<TestEntityDto> {
    const testEntity = await this.testEntityRepository.findOne({
      where: {
        id: id
      },
      relations: TestEntityDto.requiredRelations,
    });
    return TestEntityDto.fromTestEntity(testEntity);
  }

  @IntentIgnoreBody()
  async findTestEntities(): Promise<TestEntityDto[]> {
    const testEntities = await this.testEntityRepository.find({
      relations: TestEntityDto.requiredRelations,
    });
    return testEntities.map((x) => TestEntityDto.fromTestEntity(x));
  }

  @IntentIgnoreBody()
  async updateTestEntity(id: string, dto: TestEntityUpdateDto): Promise<void> {
    const existingTestEntity = await this.testEntityRepository.findOneBy({
      id: id,
    });
    existingTestEntity.id = dto.id;

    await this.testEntityRepository.save(existingTestEntity);
  }

  @IntentIgnoreBody()
  async deleteTestEntity(id: string): Promise<void> {
    const existingTestEntity = await this.testEntityRepository.findOneBy({
      id: id,
    });
    await this.testEntityRepository.remove(existingTestEntity);
  }
}
