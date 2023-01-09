using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;

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
    }
}
