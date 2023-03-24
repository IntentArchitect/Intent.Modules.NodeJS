import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UsersService } from './../users/users.service';
import { UserContext } from './../users/user-context';

@Injectable()
export class AuthService {
  constructor(
    private readonly usersService: UsersService,
    private readonly jwtService: JwtService
  ) {}

  async validateUser(username: string, pass: string): Promise<UserContext> {
    const user = await this.usersService.findOne(username);
    if (user && user.password === pass) {
      const { password, ...result } = user;
      return result;
    }
    return null;
  }

  async login(user: UserContext): Promise<{ access_token: string }> {
    const payload = { sub: user.userId, ...user };
    return {
      access_token: this.jwtService.sign(payload),
    };
  }
}