import { Module, Logger } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { UsersModules } from './users/users.modules';
import { TestEntitiesController } from './web/rest/test-entities.controller';
import { TestEntitiesService } from './services/test-entities.service';
import { BasicAuditingSubscriber } from './typeorm/basic-auditing-subscriber';
import { typeOrmConfig } from './orm.config';
import { TypeOrmExModule } from './typeorm/typeorm-ex.module';
import { TestEntityRepository } from './repository/test-entity.repository';
import { ConfigModule } from '@nestjs/config';
import { ClsModule } from 'nestjs-cls';
import { TypeOrmModule } from '@nestjs/typeorm';
import { IntentMerge } from './intent/intent.decorators';

@IntentMerge()
@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    AuthModule,
    UsersModules,
    ClsModule.forRoot({
      global: true,
      middleware: { mount: true },
    }),
    TypeOrmModule.forRoot(typeOrmConfig),
    TypeOrmExModule.forCustomRepository([
      TestEntityRepository,
    ])
  ],
  controllers: [
    TestEntitiesController
  ],
  providers: [
    Logger,
    TestEntitiesService,
    BasicAuditingSubscriber
  ]
})
export class AppModule { }