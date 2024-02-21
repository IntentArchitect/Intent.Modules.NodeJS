import { Controller, Logger, Post, Req, Request, Body, Get, Param, Put, Delete } from '@nestjs/common';
import { ClassACreateDto } from './../../services/dto/ClassAS/class-a-create.dto';
import { ClassADto } from './../../services/dto/ClassAS/class-a.dto';
import { ClassAUpdateDto } from './../../services/dto/ClassAS/class-a-update.dto';
import { ClassASService } from './../../services/class-as.service';
import { JsonResponse } from './json-response';
import { ApiTags, ApiCreatedResponse, ApiBadRequestResponse, ApiOkResponse, ApiNotFoundResponse, ApiNoContentResponse } from '@nestjs/swagger';

@ApiTags('ClassAS')
@Controller('api/classas')
export class ClassASController {
  logger = new Logger('ClassASController');

  constructor(private readonly classASService: ClassASService) {}

  @Post('')
  @ApiCreatedResponse({
    description: 'The record has been successfully created.',
    type: String,
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async create(@Req() req: Request, @Body() dto: ClassACreateDto): Promise<JsonResponse<string>> {
    const result = await this.classASService.create(dto);
    return new JsonResponse<string>(result);
  }

  @Get(':id')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: ClassADto,
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  @ApiNotFoundResponse({ description: 'Response not found.' })
  async findById(@Req() req: Request, @Param('id') id: string): Promise<ClassADto> {
    const result = await this.classASService.findById(id);
    return result;
  }

  @Get('')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: ClassADto,
    isArray: true,
  })
  async findAll(@Req() req: Request): Promise<ClassADto[]> {
    const result = await this.classASService.findAll();
    return result;
  }

  @Put(':id')
  @ApiNoContentResponse({
    description: 'Successfully updated.',
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async put(@Req() req: Request, @Param('id') id: string, @Body() dto: ClassAUpdateDto): Promise<void> {
    return await this.classASService.put(id, dto);
  }

  @Delete(':id')
  @ApiOkResponse({
    description: 'Successfully deleted.',
    type: ClassADto,
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async delete(@Req() req: Request, @Param('id') id: string): Promise<ClassADto> {
    const result = await this.classASService.delete(id);
    return result;
  }
}