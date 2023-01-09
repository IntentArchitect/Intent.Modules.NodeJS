using System.Linq;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.TypeScript.Builder;
using Intent.Modules.Common.TypeScript.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK.Templates.Stack.Interceptors
{
    internal class LambdaInterceptor : IStackTemplateInterceptor
    {
        private readonly StackTemplate _stackTemplate;

        public LambdaInterceptor(StackTemplate stackTemplate)
        {
            _stackTemplate = stackTemplate;
        }

        public void Apply(TypescriptConstructor constructor)
        {
            var lambdaFunctions = _stackTemplate.Model.InternalElement.GetSelfAndChildElementsOfType(References.Elements.LambdaFunction)
                .OrderBy(x => x.Name)
                .ToArray();

            if (lambdaFunctions.Length > 0)
            {
                constructor.Class.File.AddImport("*", "lambda", "aws-cdk-lib/aws-lambda");
                constructor.Class.File.AddImport("NodejsFunction", "aws-cdk-lib/aws-lambda-nodejs");
                constructor.Class.File.AddImport("join", "path");
            }

            foreach (var lambdaFunction in lambdaFunctions)
            {
                var handlerTemplate = _stackTemplate.GetTemplate<IClassProvider>("Distribution.Functions.Handler", lambdaFunction, new TemplateDiscoveryOptions
                {
                    TrackDependency = false
                });
                var variableName = lambdaFunction.Name.ToCamelCase();
                var relativePath = _stackTemplate.GetRelativePath(handlerTemplate);
                var exportedTypeName = handlerTemplate.ClassName;

                constructor.AddStatement(@$"const {variableName}Function = new NodejsFunction(this, '{lambdaFunction.Name.ToPascalCase()}Handler', {{
            entry: join(__dirname, '{relativePath}'),
            handler: '{exportedTypeName}'
        }});", statement => statement
                    .SeparatedFromPrevious()
                    .AddMetadata("SourceElement", lambdaFunction)
                    .AddMetadata("VariableName", variableName)
                );
            }
        }
    }
}
