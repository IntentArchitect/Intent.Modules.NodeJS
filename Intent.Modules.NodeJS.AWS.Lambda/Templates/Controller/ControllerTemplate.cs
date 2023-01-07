using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Modules.Common.Templates;
using Intent.Modules.TypeScript.Weaving.Decorators;
using Intent.RoslynWeaver.Attributes;
using Microsoft.VisualBasic;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Controller
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class ControllerTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"export class {ClassName} {{{GetConstructor()}
    {this.IntentIgnoreBodyDecorator()}
    async handle({string.Join(", ", GetParameters())}): Promise<{GetReturnType()}> {{
        {GetBody()}
    }}
}}
";
        }

        private string GetConstructor()
        {
            var parameters = _dependencyResolvers
                .SelectMany(x => x.GetConstructorParameters(), (_, x) => $"{Environment.NewLine}        private readonly {x},")
                .ToArray();
            if (parameters.Length == 0)
            {
                return string.Empty;
            }

            return @$"
    constructor({string.Concat(parameters)}
    ) {{ }}
";
        }

        private string GetBody()
        {
            var implementation = GetDecorators()
                .Select(x => x.GetImplementation())
                .SingleOrDefault(x => !string.IsNullOrWhiteSpace(x));

            return implementation ?? "throw new Error(\"Not implemented\");";
        }

        private IEnumerable<string> GetParameters() => Model.Parameters
            .Select(parameter => $"{parameter.Name.ToCamelCase()}: {GetTypeName(parameter)}");

        private string GetReturnType() => Model.TypeReference?.Element != null
            ? GetTypeName(Model)
            : "void";
    }
}