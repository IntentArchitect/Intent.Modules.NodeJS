namespace Intent.Modules.NestJS.Core.Events;

public class NestJsProviderRequest
{
    public NestJsProviderRequest(string type, string importFromLocation)
    {
        Type = type;
        ImportFromLocation = importFromLocation;
    }

    public string Type { get; set; }
    public string ImportFromLocation { get; set; }
}