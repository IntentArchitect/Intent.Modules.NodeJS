using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.AWS.Lambda.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementExtensionModel", Version = "1.0")]

namespace Intent.NodeJS.AWS.Lambda.Api
{
    [IntentManaged(Mode.Fully, Signature = Mode.Fully)]
    public class NodeJSLambdaExtensionsModel : LambdaFunctionModel
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public NodeJSLambdaExtensionsModel(IElement element) : base(element)
        {
        }

    }
}