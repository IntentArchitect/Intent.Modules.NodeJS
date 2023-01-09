using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.CDK.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class StackTemplateRegistration : FilePerModelTemplateRegistration<StackModel>
    {
        private readonly IMetadataManager _metadataManager;

        public StackTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }
        public override string TemplateId => StackTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, StackModel model)
        {
            return new StackTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<StackModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetStackModels();
        }
    }
}