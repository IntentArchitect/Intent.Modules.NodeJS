using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class HandlerTemplateRegistration : FilePerModelTemplateRegistration<LambdaFunctionModel>
    {
        private readonly IMetadataManager _metadataManager;

        public HandlerTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => HandlerTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, LambdaFunctionModel model)
        {
            return new HandlerTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<LambdaFunctionModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetLambdaFunctionModels();
        }
    }
}