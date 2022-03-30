using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.Modules.NestJS.Core.Resources;
using Intent.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.Custom", Version = "1.0")]

namespace Intent.Modules.NestJS.Core.Templates.ZipFileContent
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class ZipFileContentTemplateRegistration : ITemplateRegistration
    {
        private readonly IMetadataManager _metadataManager;

        public ZipFileContentTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public string TemplateId => ZipFileContentTemplate.TemplateId;

        [IntentManaged(Mode.Ignore)]
        private static readonly IReadOnlyCollection<string> TargetFolders = new string[]
        {
            "Quickstart",
            "Views",
            "wwwroot"
        };

        public void DoRegistration(ITemplateInstanceRegistry registry, IApplication applicationManager)
        {
            ResourceHelper.ReadQuickstartFileContents(archive =>
            {
                foreach (var entry in archive.Entries.Where(p => p.Name != string.Empty))
                {
                    registry.RegisterTemplate(TemplateId, project => new ZipFileContentTemplate(project, new ZipEntry
                    {
                        FullFileNamePath = entry.FullName
                            .Replace(ResourceHelper.ZipFileName + "/", string.Empty),
                        Content = new StreamReader(entry.Open()).ReadToEnd()
                    }));
                }
            });
        }
    }
}