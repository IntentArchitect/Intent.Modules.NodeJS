import { TestEntity } from './../../../domain/entities/test-entity.entity';
import { ApiProperty } from '@nestjs/swagger';
import { IsBoolean, IsNumber, IsDate, IsInt, IsString } from 'class-validator';
import { Type } from 'class-transformer';

export class TestEntityCreateDto {
  static fromTestEntity(testEntity: TestEntity): TestEntityCreateDto {
    if (testEntity == null) {
      return null;
    }
    const dto = new TestEntityCreateDto();
    
    return dto;
  }

  static requiredRelations: string[] = [];
}