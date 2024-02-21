import { DataSource } from 'typeorm';
import { typeOrmConfig } from './orm.config';

export default new DataSource(typeOrmConfig as any);