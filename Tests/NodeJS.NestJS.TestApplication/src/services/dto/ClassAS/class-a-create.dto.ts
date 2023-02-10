import { ClassA } from './../../../domain/entities/class-a.entity';
import { IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class ClassACreateDto {
  @IsString()
  @ApiProperty()
  attribute: string;

  static fromClassA(classA: ClassA): ClassACreateDto {
    if (classA == null) {
      return null;
    }
    const dto = new ClassACreateDto();
    dto.attribute = classA.attribute;
    return dto;
  }

  static requiredRelations: string[] = [];
}