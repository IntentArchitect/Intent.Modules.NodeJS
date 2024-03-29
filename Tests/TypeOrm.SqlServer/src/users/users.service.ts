import { Injectable, Logger } from '@nestjs/common';
import { UserContext } from './user-context';
import { IntentIgnore } from './../intent/intent.decorators';

@Injectable()
@IntentIgnore()
export class UsersService {
  private readonly users: UserContext[];
  private readonly logger = new Logger(UsersService.name);

  constructor() {
    this.logger.warn('Using example users list, replace with your own implementation.');

    this.users = [
      {
        userId: 1,
        username: 'john',
        password: 'changeme',
      },
      {
        userId: 2,
        username: 'chris',
        password: 'secret',
      },
      {
        userId: 3,
        username: 'maria',
        password: 'guess',
      },
    ];
  }

  async findOne(username: string): Promise<UserContext | undefined> {
    return this.users.find(user => user.username === username);
  }
}
