using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NestJS.Winston.Templates.WinstonOptions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NestJS.Winston.Templates
{
    public static class TemplateExtensions
    {
        public static string GetWinstonOptionsName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(WinstonOptionsTemplate.TemplateId);
        }

    }
}