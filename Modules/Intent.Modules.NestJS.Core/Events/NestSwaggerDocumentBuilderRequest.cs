namespace Intent.Modules.NestJS.Core.Events;

public class NestSwaggerDocumentBuilderRequest
{
    public NestSwaggerDocumentBuilderRequest(string method)
    {
        Method = method;
    }

    public string Method { get; set; }
}