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
            var outputTargetFolders = template.OutputTarget.GetTargetPath()
                .SkipWhile(x => !"${stackName}".Contains(x.Name, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Name.Replace("${stackName}", package.Name, StringComparison.OrdinalIgnoreCase))
                .ToArray();
            var parentFolders = Enumerable.Range(0, outputTargetFolders.Length).Select(_ => "..");

            var allAdditionalFolders = template.Model is IHasFolder hasFolder
                ? hasFolder.GetParentFolderNames().Concat(additionalFolders)
                : additionalFolders;

            var allPathParts = parentFolders
                .Concat(outputTargetFolders)
                .Concat(allAdditionalFolders);

            return string.Join('/', allPathParts).Replace("${stackName}", package.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
