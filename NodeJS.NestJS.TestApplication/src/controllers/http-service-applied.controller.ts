import { Controller, Logger, Get, Req, Request, Post, Query } from '@nestjs/common';
import { HttpServiceAppliedService } from './http-service-applied.service';

@Controller('api/httpserviceapplied')
export class HttpServiceAppliedController {
  logger = new Logger('HttpServiceAppliedController');

  constructor(private readonly httpServiceAppliedService: HttpServiceAppliedService) {}

  @Get("")
  async getValue(@Req() req: Request): Promise<string> {
    var result = await this.httpServiceAppliedService.getValue();
    return result;
  }

  @Post("")
  async postValue(@Req() req: Request, @Query('value') value: string): Promise<void> {
    return await this.httpServiceAppliedService.postValue(value);
  }
}