import { TestEntity } from './../../../domain/entities/test-entity.entity';
import { ApiProperty } from '@nestjs/swagger';
import { IsBoolean, IsNumber, IsDate, IsInt, IsString } from 'class-validator';
import { Type } from 'class-transformer';

export class TestEntityDto {
  @ApiProperty()
  id: string;

  @ApiProperty()
  binaryField: any;

  @IsBoolean()
  @ApiProperty()
  boolField: boolean;

  @IsNumber()
  @ApiProperty()
  byteField: number;

  @ApiProperty()
  charField: number;

  @IsDate()
  @ApiProperty()
  @Type(() => Date)
  dateField: Date;

  @IsDate()
  @ApiProperty()
  @Type(() => Date)
  datetimeField: Date;

  @IsDate()
  @ApiProperty()
  @Type(() => Date)
  datetimeoffsetField: any;

  @IsNumber()
  @ApiProperty()
  decimalField: number;

  @IsNumber()
  @ApiProperty()
  doubleField: number;

  @IsNumber()
  @ApiProperty()
  floatField: number;

  @ApiProperty()
  guidField: string;

  @IsNumber()
  @IsInt()
  @ApiProperty()
  intField: number;

  @IsNumber()
  @ApiProperty()
  longField: number;

  @IsNumber()
  @ApiProperty()
  shortField: number;

  @IsString()
  @ApiProperty()
  stringField: string;

  static fromTestEntity(testEntity: TestEntity): TestEntityDto {
    if (testEntity == null) {
      return null;
    }
    const dto = new TestEntityDto();
    dto.id = testEntity.id;
    dto.binaryField = testEntity.binaryField;
    dto.boolField = testEntity.boolField;
    dto.byteField = testEntity.byteField;
    dto.charField = testEntity.charField;
    dto.dateField = testEntity.dateField;
    dto.datetimeField = testEntity.datetimeField;
    dto.datetimeoffsetField = testEntity.datetimeoffsetField;
    dto.decimalField = testEntity.decimalField;
    dto.doubleField = testEntity.doubleField;
    dto.floatField = testEntity.floatField;
    dto.guidField = testEntity.guidField;
    dto.intField = testEntity.intField;
    dto.longField = testEntity.longField;
    dto.shortField = testEntity.shortField;
    dto.stringField = testEntity.stringField;
    return dto;
  }

  static requiredRelations: string[] = [];
}