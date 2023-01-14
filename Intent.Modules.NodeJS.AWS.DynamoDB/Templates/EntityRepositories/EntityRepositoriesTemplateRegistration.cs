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

namespace Intent.Modules.NodeJS.AWS.DynamoDB.Templates.EntityRepositories
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class EntityRepositoriesTemplateRegistration : FilePerModelTemplateRegistration<DynamoDBTableModel>
    {
        private readonly IMetadataManager _metadataManager;

        public EntityRepositoriesTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => EntityRepositoriesTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, DynamoDBTableModel model)
        {
            return new EntityRepositoriesTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<DynamoDBTableModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetDynamoDBTableModels();
        }
    }
}