using System.Collections.Generic;

namespace Intent.Modules.NestJS.Core.Events;

public class NestApplicationGlobalPipeRequest
{
    public NestApplicationGlobalPipeRequest(
        string globalPipe,
        IEnumerable<(string Type, string Location)> typesToImport)
    {
        GlobalPipe = globalPipe;
        TypesToImport = typesToImport;
    }

    public string GlobalPipe { get; }
    public IEnumerable<(string Type, string Location)> TypesToImport { get; }
}