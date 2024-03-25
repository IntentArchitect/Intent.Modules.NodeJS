import { Controller, Logger, Get, Req, Request, Post, Query } from '@nestjs/common';
import { HttpServiceAppliedService } from './../../services/http-service-applied.service';
import { ApiTags, ApiOkResponse, ApiNotFoundResponse, ApiCreatedResponse, ApiBadRequestResponse } from '@nestjs/swagger';

@ApiTags('HttpServiceApplied')
@Controller('api/httpserviceapplied')
export class HttpServiceAppliedController {
  logger = new Logger('HttpServiceAppliedController');

  constructor(private readonly httpServiceAppliedService: HttpServiceAppliedService) {}

  @Get('')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: String,
  })
  @ApiNotFoundResponse({ description: 'Response not found.' })
  async getValue(@Req() req: Request): Promise<string> {
    const result = await this.httpServiceAppliedService.getValue();
    return result;
  }

  @Post('')
  @ApiCreatedResponse({
    description: 'The record has been successfully created.',
  })
  @ApiBadRequestResponse({ description: 'Bad request.' })
  async postValue(@Req() req: Request, @Query('value') value: string): Promise<void> {
    return await this.httpServiceAppliedService.postValue(value);
  }

  @Get('num-array')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: Number,
    isArray: true,
  })
  async getNumberArray(@Req() req: Request): Promise<number[]> {
    const result = await this.httpServiceAppliedService.getNumberArray();
    return result;
  }

  @Get('string-array')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: 'String',
    isArray: true,
  })
  async getStringArray(@Req() req: Request): Promise<string[]> {
    const result = await this.httpServiceAppliedService.getStringArray();
    return result;
  }

  @Get('object-array')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: Object,
    isArray: true,
  })
  async getObjectArray(@Req() req: Request): Promise<any[]> {
    const result = await this.httpServiceAppliedService.getObjectArray();
    return result;
  }

  @Get('decimal-array')
  @ApiOkResponse({
    description: 'Result retrieved successfully.',
    type: Number,
    isArray: true,
  })
  async getDecimalArray(@Req() req: Request): Promise<number[]> {
    const result = await this.httpServiceAppliedService.getDecimalArray();
    return result;
  }
}