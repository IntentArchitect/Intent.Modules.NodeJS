using Intent.Engine;
using Intent.Modules.Npm;
using Intent.Modules.Npm.Templates.PackageJsonFile;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class TypeOrmPackageJsonFileDecorator : PackageJsonFileDecorator
    {
        [IntentManaged(Mode.Fully)]
        public const string DecoratorId = "Intent.NodeJS.TypeORM.TypeOrmPackageJsonFileDecorator";

        [IntentManaged(Mode.Fully)]
        private readonly PackageJsonFileTemplate _template;
        [IntentManaged(Mode.Fully)]
        private readonly IApplication _application;

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public TypeOrmPackageJsonFileDecorator(PackageJsonFileTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override void UpdateSettings(JsonEditor fileEditor)
        {
            var scripts = new JsonEditor(fileEditor.GetProperty("scripts"));
            scripts.AddPropertyIfNotExists("typeorm", "ts-node ./node_modules/typeorm/cli");
            scripts.AddPropertyIfNotExists("typeorm:run-migrations", "npm run typeorm migration:run -- -d ./src/datasource.ts");
            scripts.AddPropertyIfNotExists("typeorm:generate-migration", "npm run typeorm -- -d ./src/datasource.ts migration:generate ./src/migrations/%npm_config_name%");
            scripts.AddPropertyIfNotExists("typeorm:create-migration", "npm run typeorm -- migration:create ./src/migrations/%npm_config_name%");
            scripts.AddPropertyIfNotExists("typeorm:revert-migration", "npm run typeorm -- -d ./src/datasource.ts migration:revert");
        }
    }
}