namespace Intent.Modules.NestJS.Core.Events
{
    public class NestJsProviderCreatedEvent
    {
        public NestJsProviderCreatedEvent(string moduleId, string templateId, string modelId)
        {
            ModuleId = moduleId;
            TemplateId = templateId;
            ModelId = modelId;
        }

        public string ModuleId { get; set; }
        public string TemplateId { get; set; }
        public string ModelId { get; set; }
    }
}