import { ClassA } from './../../../domain/entities/class-a.entity';
import { ApiProperty } from '@nestjs/swagger';
import { IsString } from 'class-validator';

export class ClassADto {
  @ApiProperty()
  id: string;

  @IsString()
  @ApiProperty()
  attribute: string;

  static fromClassA(classA: ClassA): ClassADto {
    if (classA == null) {
      return null;
    }
    const dto = new ClassADto();
    dto.id = classA.id;
    dto.attribute = classA.attribute;
    return dto;
  }

  static requiredRelations: string[] = [];
}