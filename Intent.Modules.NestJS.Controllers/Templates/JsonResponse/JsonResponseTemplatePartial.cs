using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.WebApi.Api;
using Intent.Modelers.Services.Api;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplatePartial", Version = "1.0")]

namespace Intent.Modules.NestJS.Controllers.Templates.JsonResponse
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class JsonResponseTemplate : TypeScriptTemplateBase<object>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.NodeJS.NestJS.Controllers.JsonResponse";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public JsonResponseTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public override ITemplateFileConfig GetTemplateFileConfig()
        {
            return new TypeScriptFileConfig(
                className: $"JsonResponse",
                fileName: $"json-response"
            );
        }

        public override bool CanRunTemplate()
        {
            return ExecutionContext
                .MetadataManager
                .Services(ExecutionContext.GetApplicationConfig().Id)
                .GetServiceModels()
                .SelectMany(s => s.Operations)
                .Any(p => p.GetHttpSettings()?.ReturnTypeMediatype()?.IsApplicationJson() == true);
        }
    }
}