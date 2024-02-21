import { Controller, Logger, Post, Req, Request, Body, Get, Param, Put, Delete } from '@nestjs/common';
import { TestEntityCreateDto } from './../../services/dto/TestEntities/test-entity-create.dto';
import { TestEntityDto } from './../../services/dto/TestEntities/test-entity.dto';
import { TestEntityUpdateDto } from './../../services/dto/TestEntities/test-entity-update.dto';
import { TestEntitiesService } from './../../services/test-entities.service';
import { ApiTags, ApiCreatedResponse, ApiBadRequestResponse, ApiOkResponse, ApiNotFoundResponse, ApiNoContentResponse, ApiResponse } from '@nestjs/swagger';

@ApiTags('TestEntities')
@Controller('api/test-entities')
export class TestEntitiesController {
  logger = new Logger('TestEntitiesController');

  constructor(private readonly testEntitiesService: TestEntitiesService) {}

  @Post('')
  @ApiCreatedResponse({
    description: 'The record has been successfully created.',
    type: String,
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async createTestEntity(@Req() req: Request, @Body() dto: TestEntityCreateDto): Promise<string> {
    const result = await this.testEntitiesService.createTestEntity(dto);
    return result;
  }

  @Get(':id')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: TestEntityDto,
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  @ApiNotFoundResponse({ description: 'Response not found.' })
  async findTestEntityById(@Req() req: Request, @Param('id') id: string): Promise<TestEntityDto> {
    const result = await this.testEntitiesService.findTestEntityById(id);
    return result;
  }

  @Get('')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: TestEntityDto,
    isArray: true,
  })
  async findTestEntities(@Req() req: Request): Promise<TestEntityDto[]> {
    const result = await this.testEntitiesService.findTestEntities();
    return result;
  }

  @Put(':id')
  @ApiNoContentResponse({
    description: 'Successfully updated.',
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async updateTestEntity(@Req() req: Request, @Param('id') id: string, @Body() dto: TestEntityUpdateDto): Promise<void> {
    return await this.testEntitiesService.updateTestEntity(id, dto);
  }

  @Delete(':id')
  @ApiOkResponse({
    description: 'Successfully deleted.',
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async deleteTestEntity(@Req() req: Request, @Param('id') id: string): Promise<void> {
    return await this.testEntitiesService.deleteTestEntity(id);
  }
}