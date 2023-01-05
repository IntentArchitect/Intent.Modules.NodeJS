using System;
using System.Collections.Generic;
using System.Linq;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.TypeScript.Templates.TypescriptTemplateStringInterpolation", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.Lambda.Templates.Middyfy
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class MiddyfyTemplate
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public override string TransformText()
        {
            return $@"
import middy from '@middy/core'
import middyJsonBodyParser from '@middy/http-json-body-parser'

export const {ClassName} middyfy = (handler: any{GetParameters()}) => {{
    const newHandler = middy(handler).use(middyJsonBodyParser());{GetStatements()}

    return newHandler;
}}
";
        }

        private string GetParameters()
        {
            return string.Concat(GetDecorators()
                .SelectMany(x => x.GetParameters(), (_, x) => $", {x}"));
        }

        private string GetStatements()
        {
            return string.Concat(GetDecorators()
                .SelectMany(x => x.GetStatements(), (_, x) => $"{Environment.NewLine}    {x}"));
        }
    }
}