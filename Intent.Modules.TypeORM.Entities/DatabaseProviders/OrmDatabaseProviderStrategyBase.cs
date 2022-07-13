using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;
using Intent.Modules.TypeORM.Entities.Api;

namespace Intent.Modules.TypeORM.Entities.DatabaseProviders
{
    internal abstract class OrmDatabaseProviderStrategyBase : IOrmDatabaseProviderStrategy
    {
        protected OrmDatabaseProviderStrategyBase(IOutputTarget outputTarget)
        {
            OutputTarget = outputTarget;
        }

        protected IOutputTarget OutputTarget { get; }

        public bool TryGetColumnType(AttributeModel attribute,
            out (string Type, IEnumerable<string> AdditionalOptions) columnType)
        {
            if (attribute.TryGetColumnOptions(out var stereotype) &&
                !string.IsNullOrWhiteSpace(stereotype.Type()))
            {
                var additionalOptions = !string.IsNullOrWhiteSpace(stereotype.AdditionalOptions())
                    ? stereotype
                        .AdditionalOptions()
                        .Split(",")
                        .Select(x => x.Trim())
                    : Enumerable.Empty<string>();

                columnType = (stereotype.Type(), additionalOptions);
                return true;
            }

            columnType = default;

            var typeName = attribute.Type?.Element?.Name;
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return false;
            }

            return TryGetColumnType(typeName, out columnType);
        }

        public abstract IEnumerable<EnvironmentVariableRequest> GetEnvironmentVariableRequests();
        public abstract IEnumerable<string> GetConfigurationOptions();
        public abstract IEnumerable<NpmPackageDependency> GetPackageDependencies();
        public abstract bool TryGetColumnType(string typeName, out (string Type, IEnumerable<string> AdditionalOptions) columnType);
    }
}
