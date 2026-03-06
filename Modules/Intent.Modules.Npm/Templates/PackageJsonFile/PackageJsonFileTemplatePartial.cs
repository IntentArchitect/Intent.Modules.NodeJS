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
using Newtonsoft.Json.Linq;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.Npm.Templates.PackageJsonFile
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class PackageJsonFileTemplate : IntentTemplateBase<object, PackageJsonFileDecorator>
    {
        private readonly ICollection<NpmPackageDependency> _dependencies = new List<NpmPackageDependency>();

        private readonly ICollection<NpmPackageScript> _scripts = new List<NpmPackageScript>();

        private readonly ICollection<NpmPackageEntry> _entries = new List<NpmPackageEntry>();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Npm.PackageJsonFile";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public PackageJsonFileTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<NpmPackageDependency>(@package => _dependencies.Add(@package));
            ExecutionContext.EventDispatcher.Subscribe<NpmPackageScript>(script => _scripts.Add(script));
            ExecutionContext.EventDispatcher.Subscribe<NpmPackageEntry>(entry => _entries.Add(entry));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TemplateFileConfig(
                fileName: $"package",
                fileExtension: "json"
            );
        }

        public override string RunTemplate()
        {
            var content = TryGetExistingFileContent(out var existingContent)
                ? existingContent
                : TransformText();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
            var jsonEditor = new JsonEditor(jsonObject);

            var scripts = new JsonEditor(jsonEditor.GetProperty("scripts"));
            var dependencies = new JsonEditor(jsonEditor.GetProperty("dependencies"));
            var devDependencies = new JsonEditor(jsonEditor.GetProperty("devDependencies"));

            foreach (var script in _scripts)
            {
                scripts.AddPropertyIfNotExists(script.Name, script.Command);
            }

            foreach (var entry in _entries)
            {
                jsonEditor.AddPropertyIfNotExists(entry.Name, entry.Value);
            }

            foreach (var dependency in _dependencies)
            {
                if (dependency.IsDevDependency)
                {
                    devDependencies.AddOrUpdateDependency(dependency);
                }
                else
                {
                    dependencies.AddOrUpdateDependency(dependency);
                }
            }

            foreach (var decorator in GetDecorators())
            {
                decorator.UpdateSettings(jsonEditor);
            }

            SortPropertiesOf(jsonObject, "dependencies");
            SortPropertiesOf(jsonObject, "devDependencies");

            return JsonConvert.SerializeObject(jsonEditor.Value, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            });
        }

        private static void SortPropertiesOf(JObject jObject, string propertiesOf)
        {
            jObject![propertiesOf] = new JObject(jObject[propertiesOf].Children<JProperty>()!
                .OrderBy(x => x.Name)
                .Select(x => x));
        }
    }
}