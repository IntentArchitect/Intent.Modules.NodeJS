using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NestJS.Authentication
{
    internal static class NpmPackageDependencies
    {
        public static NpmPackageDependency NestJsPassport = new("@nestjs/passport", "^9.0.0");
        public static NpmPackageDependency NestJsJwt = new("@nestjs/jwt", "^9.0.0");
        public static NpmPackageDependency Passport = new("passport", "^0.6.0");
        public static NpmPackageDependency PassportJwt = new("passport-jwt", "^4.0.0");
        public static NpmPackageDependency PassportLocal = new("passport-local", "^1.0.0");
    }
}
