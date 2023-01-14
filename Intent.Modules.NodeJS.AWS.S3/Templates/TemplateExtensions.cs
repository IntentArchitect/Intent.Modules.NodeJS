using System.Collections.Generic;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.S3.Templates.S3BucketClient;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.NodeJS.AWS.S3.Templates
{
    public static class TemplateExtensions
    {
        public static string GetS3BucketClientName<T>(this IntentTemplateBase<T> template)
        {
            return template.GetTypeName(S3BucketClientTemplate.TemplateId);
        }

    }
}