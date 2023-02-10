using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.ProjectItemTemplate.Partial", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.Dotenv
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class DotenvTemplate : IntentTemplateBase<object>
    {
        private readonly Dictionary<string, string> _requests = new();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Core.dotenv";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DotenvTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<EnvironmentVariableRequest>(Handle);
        }

        private void Handle(EnvironmentVariableRequest request)
        {
            if (_requests.TryGetValue(request.Name, out var value))
            {
                if (value != request.Value)
                {
                    throw new Exception($"Environment variable request with name already \"{request.Name}\" exists with different value \"{value}\".");
                }

                return;
            }

            _requests.Add(request.Name, request.Value);
        }

        public override string RunTemplate()
        {
            if (!TryGetExistingFileContent(out var content))
            {
                content = base.RunTemplate();
            }

            var lines = content
                .Replace("\r\n", "\n")
                .Split("\n")
                .ToList();
            var existingEntries = lines
                .Where(x => !x.Trim().StartsWith("#") && x.Contains("="))
                .Select(x => x.Split("=")[0].Trim().ToLowerInvariant())
                .ToHashSet();

            foreach (var (name, value) in _requests)
            {
                if (existingEntries.Contains(name.ToLowerInvariant()))
                {
                    continue;
                }

                lines.Add($"{name}=\"{value}\"");
            }

            return string.Join(Environment.NewLine, lines);
        }

        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TemplateFileConfig(
                fileName: string.Empty,
                fileExtension: "env"
            );
        }
    }
}