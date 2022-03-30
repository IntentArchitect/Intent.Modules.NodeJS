using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Newtonsoft.Json;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.Npm.Templates.PackageJsonFile
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class PackageJsonFileTemplate : IntentTemplateBase<object, PackageJsonFileDecorator>
    {
        private readonly ICollection<NpmPackageDependency> _dependencies = new List<NpmPackageDependency>();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Npm.PackageJsonFile";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public PackageJsonFileTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<NpmPackageDependency>(@package => _dependencies.Add(@package));
        }

        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TemplateFileConfig(
                fileName: $"package",
                fileExtension: "json"
            );
        }

        public override string RunTemplate()
        {
            var meta = GetMetadata();
            var fullFileName = Path.Combine(meta.GetFullLocationPath(), meta.FileNameWithExtension());

            var jsonObject = new JsonEditor(LoadOrCreate(fullFileName));

            var dependencies = new JsonEditor(jsonObject.GetProperty("dependencies"));
            var devDependencies = new JsonEditor(jsonObject.GetProperty("devDependencies"));
            foreach (var dependency in _dependencies)
            {
                if (dependency.IsDevDependency)
                {
                    devDependencies.AddPropertyIfNotExists(dependency.Name, dependency.Version);
                }
                else
                {
                    dependencies.AddPropertyIfNotExists(dependency.Name, dependency.Version);
                }
            }
            foreach (var decorator in GetDecorators())
            {
                decorator.UpdateSettings(jsonObject);
            }

            return JsonConvert.SerializeObject(jsonObject.Value, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            });
        }

        private dynamic LoadOrCreate(string fullFileName)
        {
            return File.Exists(fullFileName)
                ? JsonConvert.DeserializeObject(File.ReadAllText(fullFileName))
                : JsonConvert.DeserializeObject(TransformText());
        }
    }
}