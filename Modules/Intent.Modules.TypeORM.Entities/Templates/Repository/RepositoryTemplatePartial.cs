using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.Repository
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class RepositoryTemplate : TypeScriptTemplateBase<Intent.Modelers.Domain.Api.ClassModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.TypeORM.Repository";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public RepositoryTemplate(IOutputTarget outputTarget, Intent.Modelers.Domain.Api.ClassModel model) : base(TemplateId, outputTarget, model)
        {
            AddDependency(NpmPackageDependencies.TypeOrm);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.ToPascalCase()}Repository",
                fileName: $"{Model.Name.ToKebabCase()}.repository"
            );
        }

        private string GetGenericTypeParameters()
        {
            if (!Model.GenericTypes.Any())
            {
                return string.Empty;
            }

            return $"<{string.Join(", ", Model.GenericTypes)}>";
        }
    }
}