using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class StackTemplateRegistration : FilePerModelTemplateRegistration<AWSPackageModel>
    {
        private readonly IMetadataManager _metadataManager;

        public StackTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }
        public override string TemplateId => StackTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, AWSPackageModel model)
        {
            return new StackTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<AWSPackageModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetAWSPackageModels();
        }
    }
}