using Intent.Metadata.Models;
using Intent.Modules.Common.Templates;

namespace Intent.Modules.NodeJS.AWS.Lambda
{
    internal static class Utils
    {
        private static readonly TemplateDiscoveryOptions OptionsWithTrackDependencies = new()
        {
            TrackDependency = true,
            ThrowIfNotFound = false
        };

        private static readonly TemplateDiscoveryOptions OptionsWithoutTrackDependencies = new()
        {
            TrackDependency = false,
            ThrowIfNotFound = false
        };

        public static bool TryGetTemplate<TTemplate, TModel>(this IntentTemplateBase<TModel> fromTemplate, string templateId, out TTemplate template, bool trackDependencies)
            where TTemplate : class
        {
            template = fromTemplate.GetTemplate<TTemplate>(
                templateId: templateId,
                options: trackDependencies ? OptionsWithTrackDependencies : OptionsWithoutTrackDependencies);
            return template != null;
        }

        public static bool TryGetTemplate<TTemplate, TModel>(this IntentTemplateBase<TModel> fromTemplate, string templateId, IMetadataModel model, out TTemplate template, bool trackDependencies)
            where TTemplate : class
        {
            template = fromTemplate.GetTemplate<TTemplate>(
                templateId: templateId,
                model: model,
                options: trackDependencies ? OptionsWithTrackDependencies : OptionsWithoutTrackDependencies);
            return template != null;
        }

        public static bool TryGetTemplate<TTemplate, TModel>(this IntentTemplateBase<TModel> fromTemplate, string templateId, string modelId, out TTemplate template, bool trackDependencies)
            where TTemplate : class
        {
            template = fromTemplate.GetTemplate<TTemplate>(
                templateId: templateId,
                modelId: modelId,
                options: trackDependencies ? OptionsWithTrackDependencies : OptionsWithoutTrackDependencies);
            return template != null;
        }
    }
}
