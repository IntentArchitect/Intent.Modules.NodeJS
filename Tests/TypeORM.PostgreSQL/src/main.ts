import { NestFactory } from '@nestjs/core';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import { ConfigService } from '@nestjs/config';
import { AppModule } from './app.module';
import { WinstonModule } from 'nest-winston';
import { WinstonOptions } from './winston.config';
import { ValidationPipe } from '@nestjs/common';
import { TestEntityCreateDto } from './services/dto/TestEntities/test-entity-create.dto';
import { TestEntityUpdateDto } from './services/dto/TestEntities/test-entity-update.dto';
import { TestEntityDto } from './services/dto/TestEntities/test-entity.dto';

async function bootstrap() {
  const app = await NestFactory.create(AppModule, {
    logger: WinstonModule.createLogger(WinstonOptions),
  });
  app.useGlobalPipes(new ValidationPipe());

  const config = new DocumentBuilder()
    .setTitle('TypeORM.PostgreSQL')
    .setDescription('')
    .setVersion('1.0')
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('swagger', app, document);

  const configService = app.get(ConfigService);
  const port = configService.get<number>('HOST_PORT', 3000);
  
  await app.listen(port);
}
bootstrap();