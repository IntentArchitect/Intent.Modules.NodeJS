import { TestEntity } from './../../../domain/entities/test-entity.entity';
import { ApiProperty } from '@nestjs/swagger';
import { IsBoolean, IsNumber, IsDate, IsInt, IsString } from 'class-validator';
import { Type } from 'class-transformer';

export class TestEntityUpdateDto {
  @ApiProperty()
  id: string;

  static fromTestEntity(testEntity: TestEntity): TestEntityUpdateDto {
    if (testEntity == null) {
      return null;
    }
    const dto = new TestEntityUpdateDto();
    dto.id = testEntity.id;
    return dto;
  }

  static requiredRelations: string[] = [];
}