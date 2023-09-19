import { Role } from './../auth/role.enum';
export class UserContext {
  userId: number;
  username: string;
  password?: string;
  roles?: Role[];
}