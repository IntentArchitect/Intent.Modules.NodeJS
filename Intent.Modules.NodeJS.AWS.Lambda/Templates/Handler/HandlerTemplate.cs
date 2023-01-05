using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class HandlerTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"import {{ Context }} from 'aws-lambda';

const {ClassName} = async (event: {_handlerStrategy.GetEventType()}, context: Context): Promise<{_handlerStrategy.GetReturnType()}> => {{
{GetBody()}}};

export const main = {this.GetMiddyfyName()}({ClassName}{GetAdditionalMiddlewareParameters()});
";
        }

        private string GetBody()
        {
            var stringBuilder = new StringBuilder();

            foreach (var statement in _handlerStrategy.GetBeforeControllerHandleStatements())
            {
                stringBuilder.AppendLine($"    {statement}");
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine($"    const controller = new {this.GetControllerName()}({string.Join(", ", _dependencyResolvers.SelectMany(x => x.GetConstructorArguments()))});");
            var resultAssignment = Model.TypeReference.Element != null
                ? "const result = "
                : string.Empty;
            stringBuilder.AppendLine($"    {resultAssignment}await controller.handle({string.Join(", ", _handlerStrategy.GetControllerHandleArguments())});");

            var returnValue = _handlerStrategy.GetReturnValue(Model.TypeReference.Element != null ? "result" : null);
            if (string.IsNullOrWhiteSpace(returnValue) &&
                Model.TypeReference.Element != null)
            {
                returnValue = "result";
            }

            if (!string.IsNullOrWhiteSpace(returnValue))
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"    return {returnValue};");
            }

            return stringBuilder.ToString();
        }

        private string GetAdditionalMiddlewareParameters()
        {
            return string.Concat(GetDecorators().SelectMany(x => x.GetAdditionalMiddlewareParameters(), (_, x) => $", {x}"));
        }
    }
}