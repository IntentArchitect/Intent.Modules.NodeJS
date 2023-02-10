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
            var content = TryGetExistingFileContent(out var existingContent)
                ? existingContent
                : TransformText();
            var jsonObject = JsonConvert.DeserializeObject<JObject>(content);
            var jsonEditor = new JsonEditor(jsonObject);

            var dependencies = new JsonEditor(jsonEditor.GetProperty("dependencies"));
            var devDependencies = new JsonEditor(jsonEditor.GetProperty("devDependencies"));

            foreach (var dependency in _dependencies)
            {
                if (dependency.IsDevDependency)
                {
                    devDependencies.AddDependencyIfNotExists(dependency);
                }
                else
                {
                    dependencies.AddDependencyIfNotExists(dependency);
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