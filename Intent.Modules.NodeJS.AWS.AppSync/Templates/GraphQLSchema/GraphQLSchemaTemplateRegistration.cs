using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.AppSync.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.Registrations;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TemplateRegistration.FilePerModel", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.AppSync.Templates.GraphQLSchema
{
    [IntentManaged(Mode.Merge, Body = Mode.Merge, Signature = Mode.Fully)]
    public class GraphQLSchemaTemplateRegistration : FilePerModelTemplateRegistration<GraphQLEndpointModel>
    {
        private readonly IMetadataManager _metadataManager;

        public GraphQLSchemaTemplateRegistration(IMetadataManager metadataManager)
        {
            _metadataManager = metadataManager;
        }

        public override string TemplateId => GraphQLSchemaTemplate.TemplateId;

        public override ITemplate CreateTemplateInstance(IOutputTarget outputTarget, GraphQLEndpointModel model)
        {
            return new GraphQLSchemaTemplate(outputTarget, model);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override IEnumerable<GraphQLEndpointModel> GetModels(IApplication application)
        {
            return _metadataManager.Application(application).GetGraphQLEndpointModels();
        }
    }
}