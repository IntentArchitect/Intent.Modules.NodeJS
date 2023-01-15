using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intent.Modelers.Application.Api;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NodeJS.AWS.CDK;
using static System.Net.Mime.MediaTypeNames;

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Handler.Strategies
{
    internal class AppSyncStrategy : IHandlerStrategy
    {
        private readonly TypeScriptTemplateBase<LambdaFunctionModel> _template;

        public AppSyncStrategy(TypeScriptTemplateBase<LambdaFunctionModel> template)
        {
            _template = template;
        }

        public bool IsApplicable() =>
            _template.ExecutionContext.MetadataManager.Application(_template.ExecutionContext.GetApplicationConfig().Id).Elements
                .Any(x => x.SpecializationType is Constants.ElementName.GraphQLSchemaType or Constants.ElementName.GraphQLMutation &&
                          x.IsMapped &&
                          x.MappedElement?.ElementId == _template.Model.Id);

        public string GetEventType() => "any";

        public string GetReturnType() => "Promise<any>";

        public string GetReturnValue(string resultVariableName) => resultVariableName;

        public IEnumerable<string> GetBeforeControllerHandleStatements()
        {
            yield break;
        }

        public void ApplyControllerCall(StringBuilder stringBuilder, string resultAssignment)
        {
            //IHandlerStrategy.DefaultApplyControllerCall(
            //    stringBuilder: stringBuilder,
            //    resultAssignment: resultAssignment,
            //    arguments: _parameter != null
            //        ? $"event as {_template.GetTypeName(_parameter)}"
            //        : string.Empty);
        }
    }
}
