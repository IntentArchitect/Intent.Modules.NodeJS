using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Controllers.Templates.DtoModel;
using Intent.Modules.NestJS.Core.Events;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.Service
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class ServiceTemplate : TypeScriptTemplateBase<ServiceModel, ServiceDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Controllers.Service";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public ServiceTemplate(IOutputTarget outputTarget, ServiceModel model) : base(TemplateId, outputTarget, model)
        {
            AddTypeSource(DtoModelTemplate.TemplateId);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.Name.RemoveSuffix("Service", "Controller")}Service",
                fileName: $"{Model.Name.RemoveSuffix("Service", "Controller").ToKebabCase()}.service"
            );
        }

        public override void BeforeTemplateExecution()
        {
            ExecutionContext.EventDispatcher.Publish(new NestJsProviderCreatedEvent(null, TemplateId, Model.Id));
        }

        public string GetParameterDefinitions(OperationModel operation)
        {
            return string.Join(", ", operation.Parameters.Select(p => $"{p.Name.ToCamelCase()}: {GetTypeName(p.TypeReference)}"));
        }

        public string GetReturnType(OperationModel operation)
        {
            return operation.ReturnType != null ? GetTypeName(operation.ReturnType) : "void";
        }

        private string GetConstructorParameters()
        {
            return string.Join(", ", GetDecorators().SelectMany(x => x.GetConstructorParameters()).Distinct());
        }

        private string GetImplementation(OperationModel operation)
        {
            var output = GetDecorators().Aggregate(x => x.GetImplementation(operation));
            if (string.IsNullOrWhiteSpace(output))
            {
                return @"throw new Error(""Write your implementation for this service here..."");";
            }
            return output;
        }
    }
}