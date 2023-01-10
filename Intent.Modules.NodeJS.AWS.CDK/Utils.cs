using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.NodeJS.AWS.CDK.Templates.EntryPoint;
using Intent.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK
{
    internal static class Utils
    {
        public static IEnumerable<IElement> GetSelfAndChildElementsOfType(this IElement element, string type)
        {
            if (element.SpecializationType == type)
            {
                yield return element;
            }

            foreach (var childElement in element.ChildElements.SelectMany(x => x.GetSelfAndChildElementsOfType(type)))
            {
                yield return childElement;
            }
        }

        public static string GetRelativePath(this ITemplate relativeToTemplate, ITemplate template)
        {
            return Path
                .GetRelativePath(
                    relativeTo: Path.GetDirectoryName(relativeToTemplate.GetMetadata().GetFilePath())!,
                    path: template.GetMetadata().GetFilePath())
                .Replace('\\', '/');
        }
    }
}
