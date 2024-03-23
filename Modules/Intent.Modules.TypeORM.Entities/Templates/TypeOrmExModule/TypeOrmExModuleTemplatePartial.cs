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

namespace Intent.Modules.TypeORM.Entities.Templates.TypeOrmExModule
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class TypeOrmExModuleTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.TypeOrmExModule";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public TypeOrmExModuleTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            AddDependency(CoreNpmPackageDependencies.NestJs.Common);
            AddDependency(NpmPackageDependencies.NestJs.TypeOrm);
            AddDependency(NpmPackageDependencies.TypeOrm);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"TypeOrmExModule",
                fileName: $"typeorm-ex.module"
            );
        }

    }
}