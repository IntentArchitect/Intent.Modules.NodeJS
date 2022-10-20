import * as winston from 'winston';
import { utilities as nestWinstonModuleUtilities, } from 'nest-winston';

const WinstonOptions: winston.LoggerOptions = {
    transports: [
        new winston.transports.Console({
          format: winston.format.combine(
            winston.format.timestamp(),
            winston.format.ms(),
            nestWinstonModuleUtilities.format.nestLike('MyApp', { prettyPrint: true }),
          ),
        }),
      ]
};

export { WinstonOptions };