﻿using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.TypeORM.Entities;

public static class NpmPackageDependencies
{
    public static class NestJs
    {
        public static NpmPackageDependency TypeOrm = new("@nestjs/typeorm", "^10.0.2");
    }

    public static NpmPackageDependency TypeOrm = new("typeorm", "^0.3.7");
    public static NpmPackageDependency EnvVar = new("env-var", "^7.1.1");
    public static NpmPackageDependency NestJsCls = new("nestjs-cls","^3.0.4");
}