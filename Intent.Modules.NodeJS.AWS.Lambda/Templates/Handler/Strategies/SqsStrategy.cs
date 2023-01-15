using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;
using Intent.Utils;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies
{
    internal class SqsStrategy : IHandlerStrategy
    {
        private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;

        public SqsStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
        {
            _template = template;
        }

        public bool IsApplicable() => _template.Model.InternalElement.AssociatedElements
            .Any(x => x.Association.SourceEnd.TypeReference.Element is IElement { SpecializationType: Constants.ElementName.SqsQueue });

        public string GetEventType() => "any";

        public string GetReturnType() => "Promise<void>";

        public string GetReturnValue(string resultVariableName) => null;

        public IEnumerable<string> GetBeforeControllerHandleStatements() => Enumerable.Empty<string>();

        public void ApplyControllerCall(StringBuilder stringBuilder, string resultAssignment)
        {
            if (_template.Model.Parameters.Count != 1)
            {
                Logging.Log.Warning($"Expected single parameter when handling SQS messages for {_template.Model}");
            }

            var paramType = _template.Model.Parameters.FirstOrDefault()?.TypeReference;
            var recordBody = paramType?.Element?.SpecializationType == Constants.ElementName.Message
                ? "JSON.parse(record.body)"
                : "record.body";

            if (paramType?.IsCollection == true)
            {
                stringBuilder.AppendLine(@$"    controller.handle(event.Records.map(record => {recordBody});");
                return;
            }

            stringBuilder.AppendLine(@$"    for (let record of event.Records) {{
        controller.handle({recordBody});
    }}");
        }
    }
}
