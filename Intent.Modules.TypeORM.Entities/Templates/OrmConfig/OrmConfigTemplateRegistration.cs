using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Modelers.Domain.Api;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.SingleFileListModel", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Templates.OrmConfig
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class OrmConfigTemplateRegistration : SingleFileListModelTemplateRegistration<ClassModel>
    {
        public override string TemplateId => OrmConfigTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, IList<ClassModel> model)
        {
            return new OrmConfigTemplate(outputTarget, model);
        }
        private readonly IMetadataManager _metadataManager;

        public OrmConfigTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IList<ClassModel> GetModels(IApplication application)
        {
            return _metadataManager.Domain(application).GetClassModels().ToList();
        }
    }
}