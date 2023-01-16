using System;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.Types.Api;

namespace Intent.Modules.NodeJS.AWS.AppSync
{
    internal static class Utils
    {
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
