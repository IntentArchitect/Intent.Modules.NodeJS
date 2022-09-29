import { Module } from '@nestjs/common';
import { HttpServiceAppliedService } from './http-service-applied.service';
import { NonHttpServiceAppliedService } from './non-http-service-applied.service';
import { HttpServiceAppliedController } from './http-service-applied.controller';
import { IntentIgnore, IntentMerge } from './src/intent/intent.decorators';
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