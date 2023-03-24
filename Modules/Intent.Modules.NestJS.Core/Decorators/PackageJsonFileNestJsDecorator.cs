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
            scripts.AddPropertyIfNotExists("lint", "eslint \"{src,apps,libs,test}/**/*.ts\"");
            scripts.AddPropertyIfNotExists("lint:fix", "eslint \"{src,apps,libs,test}/**/*.ts\" --fix");
            scripts.AddPropertyIfNotExists("test", "jest");
            scripts.AddPropertyIfNotExists("test:watch", "jest --watch");
            scripts.AddPropertyIfNotExists("test:cov", "jest --coverage");
            scripts.AddPropertyIfNotExists("test:debug", "node --inspect-brk -r tsconfig-paths/register -r ts-node/register node_modules/.bin/jest --runInBand");
            scripts.AddPropertyIfNotExists("test:e2e", "jest --config ./test/jest-e2e.json");

            var dependencies = new JsonEditor(fileEditor.GetProperty("dependencies"));
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Swagger);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.PlatformExpress);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Core);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Common);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.SwaggerUiExpress);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.Sqlite3);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.Rxjs);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.Rimraf);
            dependencies.AddDependencyIfNotExists(NpmPackageDependencies.ReflectMetadata);

            var devDependencies = new JsonEditor(fileEditor.GetProperty("devDependencies"));
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Cli);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Schematics);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.NestJs.Testing);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Types.Express);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Types.Jest);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Types.Node);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Types.Supertest);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TypescriptEslint.EslintPlugin);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TypescriptEslint.Parser);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Eslint);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.EslintConfigPrettier);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.EslintPluginPrettier);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Jest);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Prettier);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.SuperTest);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TsconfigPaths);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TsJest);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TsLoader);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.TsNode);
            devDependencies.AddDependencyIfNotExists(NpmPackageDependencies.Typescript);

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