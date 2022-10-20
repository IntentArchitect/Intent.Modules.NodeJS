import { Injectable, ExecutionContext, CanActivate } from '@nestjs/common';
import { Reflector } from '@nestjs/core';
import { ROLES_KEY } from '../roles.decorator';
import { JwtAuthGuard } from './jwt-auth.guard';
import { Role } from './../role.enum';

@Injectable()
export class RolesGuard extends JwtAuthGuard {
  constructor(reflector: Reflector) {
    super(reflector);
  }

  async canActivate(context: ExecutionContext): Promise<boolean> {
    const requiredRoles = this.reflector.getAllAndOverride<Role[]>(ROLES_KEY, [
      context.getHandler(),
      context.getClass(),
    ]);

    if (!requiredRoles || this.isPublic(context)) {
      return true;
    }

    if (!(await super.canActivate(context))) {
      return false;
    }

    const { user } = context.switchToHttp().getRequest();
    return requiredRoles.some((role) => user.roles?.includes(role));
  }
}