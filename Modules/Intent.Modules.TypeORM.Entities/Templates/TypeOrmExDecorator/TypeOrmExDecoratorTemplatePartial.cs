using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using CoreNpmPackageDependencies = Intent.Modules.NestJS.Core.NpmPackageDependencies;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.TypeOrmExDecorator
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TypeOrmExDecoratorTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.TypeOrmExDecorator";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TypeOrmExDecoratorTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(CoreNpmPackageDependencies.NestJs.Common);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"CustomRepository",
                fileName: $"typeorm-ex.decorator"
            );
        }

    }
}