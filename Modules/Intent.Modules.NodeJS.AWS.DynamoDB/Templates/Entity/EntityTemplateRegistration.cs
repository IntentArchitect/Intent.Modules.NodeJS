using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.DynamoDB.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.Entity
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class EntityTemplateRegistration : FilePerModelTemplateRegistration<EntityModel>
    {
        private readonly IMetadataManager _metadataManager;

        public EntityTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => EntityTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, EntityModel model)
        {
            return new EntityTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<EntityModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetDynamoDBTableModels().SelectMany(x => x.Entities);
        }
    }
}