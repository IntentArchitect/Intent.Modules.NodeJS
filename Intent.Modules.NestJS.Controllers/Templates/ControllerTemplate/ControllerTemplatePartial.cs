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

namespace Intent.Modules.NestJS.Controllers.Templates.ControllerTemplate
{
    public interface IControllerTemplateDecorator
    {

    }

    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class ControllerTemplate : TypeScriptTemplateBase<ServiceModel>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NestJS.Controllers.ControllerTemplate";

        public ControllerTemplate(IOutputTarget outputTarget, ServiceModel model) : base(TemplateId, outputTarget, model)
        {
        }

        public string ServiceClassName => GetTypeName(ServiceTemplate.ServiceTemplate.TemplateId, Model);

        public string GetParameterDefinitions(OperationModel operation)
        {
            return string.Join(", ", new[] { "@Req() req: Request" }.Concat(operation.Parameters.Select(p => $"{GetParameterDefinitionDecorator(operation, p)}{p.Name.ToCamelCase()}: {GetTypeName(p.TypeReference)}")));
        }

        private string GetParameterDefinitionDecorator(OperationModel operation, ParameterModel parameter)
        {
            return "";
        }

        public string GetReturnType(OperationModel operation)
        {
            return operation.ReturnType != null ? GetTypeName(operation.ReturnType) : "void";
        }

        public string GetParameters(OperationModel operation)
        {
            return string.Join(", ", operation.Parameters.Select(p => $"{p.Name.ToCamelCase()}"));
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"{Model.GetModelName()}Controller",
                fileName: $"{Model.GetModelName().ToDotCase()}.controller"
            );
        }
    }
}