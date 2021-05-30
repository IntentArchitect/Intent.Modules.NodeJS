using System.Collections.Generic;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Templates;

namespace Intent.Modules.NestJS.Core.Events
{
    public class NestJsModuleImportRequest
    {
        public NestJsModuleImportRequest(string moduleId, string statement)
        {
            ModuleId = moduleId;
            Statement = statement;
        }

        public static NestJsModuleImportRequest Create(string moduleId, string statement)
        {
            return new NestJsModuleImportRequest(moduleId, statement);
        }

        public NestJsModuleImportRequest AddImport(string type, string location)
        {
            Imports.Add(new TypeScriptImport(type, location));
            return this;
        }

        public NestJsModuleImportRequest AddDependency(ITemplateDependency dependency)
        {
            Dependencies.Add(dependency);
            return this;
        }

        public NestJsModuleImportRequest AddDependencies(params ITemplateDependency[] dependencies)
        {
            Dependencies.AddRange(dependencies);
            return this;
        }

        public string ModuleId { get; }
        public string Statement { get; }
        public List<TypeScriptImport> Imports { get; } = new List<TypeScriptImport>();
        public List<ITemplateDependency> Dependencies { get; } = new List<ITemplateDependency>();
    }
}