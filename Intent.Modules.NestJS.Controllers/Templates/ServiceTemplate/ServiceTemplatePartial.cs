using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.ServiceTemplate
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class ServiceTemplate : TypeScriptTemplateBase<ServiceModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NestJS.Controllers.ServiceTemplate";

        public ServiceTemplate(IOutputTarget outputTarget, ServiceModel model) : base(TemplateId, outputTarget, model)
        {
        }

        public string GetParameterDefinitions(OperationModel operation)
        {
            return string.Join(", ", operation.Parameters.Select(p => $"{p.Name.ToCamelCase()}: {GetTypeName(p.TypeReference)}"));
        }

        public string GetReturnType(OperationModel operation)
        {
            return operation.ReturnType != null ? GetTypeName(operation.ReturnType) : "void";
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.GetModelName()}Service",
                fileName: $"{Model.GetModelName().ToDotCase()}.service"
            );
        }

    }
}