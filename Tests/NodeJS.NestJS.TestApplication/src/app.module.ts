import { Module, Logger } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { ClassASController } from './web/rest/class-as.controller';
import { HttpServiceAppliedController } from './web/rest/http-service-applied.controller';
import { ClassASService } from './services/class-as.service';
import { HttpServiceAppliedService } from './services/http-service-applied.service';
import { NonHttpServiceAppliedService } from './services/non-http-service-applied.service';
import { typeOrmConfig } from './orm.config';
import { TypeOrmExModule } from './typeorm/typeorm-ex.module';
import { ClassARepository } from './repository/class-a.repository';
import { BasicAuditingSubscriber } from './typeorm/basic-auditing-subscriber';
import { UsersModules } from './users/users.modules';
import { ConfigModule } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ClsModule } from 'nestjs-cls';
import { IntentMerge } from './intent/intent.decorators';

@IntentMerge()
@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    AuthModule,
    TypeOrmModule.forRoot(typeOrmConfig),
    TypeOrmExModule.forCustomRepository([
      ClassARepository,
    ]),
    ClsModule.forRoot({
      global: true,
      middleware: { mount: true },
    }),
    UsersModules
  ],
  controllers: [
    ClassASController,
    HttpServiceAppliedController
  ],
  providers: [
    ClassASService,
    HttpServiceAppliedService,
    NonHttpServiceAppliedService,
    Logger,
    BasicAuditingSubscriber
  ]
})
export class AppModule { }