using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.Types.Api;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

namespace Intent.Modules.NodeJS.AWS.CDK
{
    internal static class Utils
    {
        public static IEnumerable<IElement> GetChildElementsOfType(this IPackage package, string type)
        {
            return package.ChildElements.SelectMany(x => x.GetSelfAndChildElementsOfType(type));
        }

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

        public static string GetSubstitutedRelativePath<TModel>(
            this IntentTemplateBase<TModel> template,
            IPackage package,
            params string[] additionalFolders)
        {
            var packageName = package.Name.ToKebabCase();
            var outputTargetFolders = template.OutputTarget.GetTargetPath()
                .SkipWhile(x => !"${stackName}".Contains(x.Name, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Name.Replace("${stackName}", packageName, StringComparison.OrdinalIgnoreCase))
                .ToArray();
            var parentFolders = Enumerable.Range(0, outputTargetFolders.Length).Select(_ => "..");

            var allAdditionalFolders = template.Model is IHasFolder hasFolder
                ? hasFolder.GetParentFolderNames().Concat(additionalFolders)
                : additionalFolders;

            var allPathParts = parentFolders
                .Concat(outputTargetFolders)
                .Concat(allAdditionalFolders.Select(x => x.ToKebabCase()));

            return string.Join('/', allPathParts).Replace("${stackName}", packageName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
