using System.IO;
using Intent.Engine;
using Intent.Modules.Npm;
using Intent.Modules.Npm.Templates.PackageJsonFile;
using Intent.RoslynWeaver.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class PackageJsonFileNestJsDecorator : PackageJsonFileDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.NestJS.Core.PackageJsonFileNestJsDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly PackageJsonFileTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Merge)]
        public PackageJsonFileNestJsDecorator(PackageJsonFileTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override void UpdateSettings(JsonEditor fileEditor)
        {
            var scripts = new JsonEditor(fileEditor.GetProperty("scripts"));
            scripts.AddPropertyIfNotExists("prebuild", "rimraf dist");
            scripts.AddPropertyIfNotExists("build", "nest build");
            scripts.AddPropertyIfNotExists("format", "prettier --write \"src/**/*.ts\" \"test/**/*.ts\"");
            scripts.AddPropertyIfNotExists("start", "nest start");
            scripts.AddPropertyIfNotExists("start:dev", "nest start --watch");
            scripts.AddPropertyIfNotExists("start:debug", "nest start --debug --watch");
            scripts.AddPropertyIfNotExists("start:prod", "node dist/main");
            scripts.AddPropertyIfNotExists("lint", "eslint \"{src,apps,libs,test}/**/*.ts\" --fix");
            scripts.AddPropertyIfNotExists("test", "jest");
            scripts.AddPropertyIfNotExists("test:watch", "jest --watch");
            scripts.AddPropertyIfNotExists("test:cov", "jest --coverage");
            scripts.AddPropertyIfNotExists("test:debug", "node --inspect-brk -r tsconfig-paths/register -r ts-node/register node_modules/.bin/jest --runInBand");
            scripts.AddPropertyIfNotExists("test:e2e", "jest --config ./test/jest-e2e.json");

            var dependencies = new JsonEditor(fileEditor.GetProperty("dependencies"));
            dependencies.AddPropertyIfNotExists("@nestjs/common", "^7.6.15");
            dependencies.AddPropertyIfNotExists("@nestjs/core", "^7.6.15");
            dependencies.AddPropertyIfNotExists("@nestjs/platform-express", "^7.6.15");
            dependencies.AddPropertyIfNotExists("@nestjs/swagger", "^4.8.0");
            dependencies.AddPropertyIfNotExists("reflect-metadata", "^0.1.13");
            dependencies.AddPropertyIfNotExists("rimraf", "^3.0.2");
            dependencies.AddPropertyIfNotExists("rxjs", "^6.6.6");
            dependencies.AddPropertyIfNotExists("sqlite3", "^5.0.2");
            dependencies.AddPropertyIfNotExists("swagger-ui-express", "^4.1.6");
            dependencies.AddPropertyIfNotExists("typeorm", "^0.2.32");

            var devDependencies = new JsonEditor(fileEditor.GetProperty("devDependencies"));
            devDependencies.AddPropertyIfNotExists("@nestjs/cli", "^7.6.0");
            devDependencies.AddPropertyIfNotExists("@nestjs/schematics", "^7.3.1");
            devDependencies.AddPropertyIfNotExists("@nestjs/testing", "^7.6.15");
            devDependencies.AddPropertyIfNotExists("@types/express", "^4.17.11");
            devDependencies.AddPropertyIfNotExists("@types/jest", "^26.0.22");
            devDependencies.AddPropertyIfNotExists("@types/node", "^14.14.36");
            devDependencies.AddPropertyIfNotExists("@types/supertest", "^2.0.10");
            devDependencies.AddPropertyIfNotExists("@typescript-eslint/eslint-plugin", "^4.19.0");
            devDependencies.AddPropertyIfNotExists("@typescript-eslint/parser", "^4.19.0");
            devDependencies.AddPropertyIfNotExists("eslint", "^7.22.0");
            devDependencies.AddPropertyIfNotExists("eslint-config-prettier", "^8.1.0");
            devDependencies.AddPropertyIfNotExists("eslint-plugin-prettier", "^3.3.1");
            devDependencies.AddPropertyIfNotExists("jest", "^26.6.3");
            devDependencies.AddPropertyIfNotExists("prettier", "^2.2.1");
            devDependencies.AddPropertyIfNotExists("supertest", "^6.1.3");
            devDependencies.AddPropertyIfNotExists("ts-jest", "^26.5.4");
            devDependencies.AddPropertyIfNotExists("ts-loader", "^8.0.18");
            devDependencies.AddPropertyIfNotExists("ts-node", "^9.1.1");
            devDependencies.AddPropertyIfNotExists("tsconfig-paths", "^3.9.0");
            devDependencies.AddPropertyIfNotExists("typescript", "^4.2.3");

            fileEditor.AddPropertyIfNotExists("jest", JsonSerializer.Create().Deserialize(new JsonTextReader(
                new StringReader(@"{
    ""moduleFileExtensions"": [
      ""js"",
      ""json"",
      ""ts""
    ],
    ""rootDir"": ""src"",
    ""testRegex"": "".*\\.spec\\.ts$"",
    ""transform"": {
      ""^.+\\.(t|j)s$"": ""ts-jest""
    },
    ""collectCoverageFrom"": [
      ""**/*.(t|j)s""
    ],
    ""coverageDirectory"": ""../coverage"",
    ""testEnvironment"": ""node""
  }"))));
        }
    }
}