import { Module, Logger } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { HttpServiceAppliedController } from './web/rest/http-service-applied.controller';
import { HttpServiceAppliedService } from './services/http-service-applied.service';
import { NonHttpServiceAppliedService } from './services/non-http-service-applied.service';
import { typeOrmConfig } from './orm.config';
import { UsersModules } from './users/users.modules';
import { ConfigModule } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { IntentMerge } from './intent/intent.decorators';

@IntentMerge()
@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    AuthModule,
    TypeOrmModule.forRoot(typeOrmConfig),
    UsersModules
  ],
  controllers: [
    HttpServiceAppliedController
  ],
  providers: [
    HttpServiceAppliedService,
    NonHttpServiceAppliedService,
    Logger
  ]
})
export class AppModule { }