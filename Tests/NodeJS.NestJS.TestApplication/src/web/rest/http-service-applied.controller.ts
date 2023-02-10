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
    type: 'string',
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
}