using System.Collections.Generic;
using Intent.Modelers.Domain.Api;
using OperationModel = Intent.Modelers.Services.Api.OperationModel;

namespace Intent.Modules.NodeJS.Services.CRUD.Decorators
{
    internal interface ICrudImplementationStrategy
    {
        bool IsMatch(ClassModel targetEntity, OperationModel operation);
        IEnumerable<string> GetRequiredServices();
        string GetImplementation(ClassModel targetEntity, OperationModel operation);
    }
}