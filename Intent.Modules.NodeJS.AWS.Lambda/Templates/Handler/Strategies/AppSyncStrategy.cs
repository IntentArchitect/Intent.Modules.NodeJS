using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intent.Metadata.Models;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies
{
    internal class AppSyncStrategy : IHandlerStrategy
    {
        private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;
        private readonly IElement _operation;

        public AppSyncStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
        {
            _template = template;
            _operation = _template.ExecutionContext.MetadataManager.Application(_template.ExecutionContext.GetApplicationConfig().Id).Elements
                .FirstOrDefault(x => x.SpecializationType is Constants.ElementName.GraphQLSchemaType or Constants.ElementName.GraphQLMutation &&
                          x.IsMapped &&
                          x.MappedElement?.ElementId == _template.Model.Id);
        }

        public bool IsApplicable() => _operation != null;

        public string GetEventType() => "any";

        public string GetReturnType() => "Promise<any>";

        public string GetReturnValue(string resultVariableName) => resultVariableName;

        public IEnumerable<string> GetBeforeControllerHandleStatements()
        {
            yield break;
        }

        public void ApplyControllerCall(StringBuilder stringBuilder, string resultAssignment)
        {
            var arguments = _operation.ChildElements
                .Where(x => x.SpecializationType == Constants.ElementName.Parameter)
                .Select(x => $"{Environment.NewLine}        event.arguments['{x.Name}']")
                .DefaultIfEmpty()
                .Aggregate((x, y) => $"{x}, {y}");

            stringBuilder.AppendLine($"    {resultAssignment}await controller.handle({arguments});");
        }
    }
}
