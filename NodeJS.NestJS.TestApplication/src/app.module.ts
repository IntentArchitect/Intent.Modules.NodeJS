import { Module } from '@nestjs/common';
import { HttpServiceAppliedService } from './controllers/http-service-applied.service';
import { NonHttpServiceAppliedService } from './controllers/non-http-service-applied.service';
import { HttpServiceAppliedController } from './controllers/http-service-applied.controller';
import { IntentIgnore, IntentMerge } from './intent/intent.decorators';
import { ConfigModule } from '@nestjs/config';

@IntentMerge()
@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true })
  ],
  controllers: [
    HttpServiceAppliedController
  ],
  providers: [
    HttpServiceAppliedService,
    NonHttpServiceAppliedService
  ]
})
export class AppModule { }