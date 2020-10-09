using Intent.Modelers.Services.Api;

namespace Intent.Modules.NestJS.Controllers.Templates
{
    public static class ServiceModelExtensions
    {
        public static string GetModelName(this ServiceModel model)
        {
            return model.Name.EndsWith("Service")
                ? model.Name.Substring(0, model.Name.Length - "Service".Length)
                : model.Name.EndsWith("Controller")
                    ? model.Name.Substring(0, model.Name.Length - "Controller".Length) 
                    : model.Name;
        }
    }
}