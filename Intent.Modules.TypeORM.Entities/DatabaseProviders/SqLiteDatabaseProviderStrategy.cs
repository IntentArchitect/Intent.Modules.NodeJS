using System.Collections.Generic;
using Intent.Engine;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;

namespace Intent.Modules.TypeORM.Entities.DatabaseProviders;

internal class SqLiteDatabaseProviderStrategy : OrmDatabaseProviderStrategyBase
{
    public SqLiteDatabaseProviderStrategy(IOutputTarget outputTarget) : base(outputTarget)
    {
    }

    public override IEnumerable<string> GetConfigurationOptions()
    {
        yield return "type: 'sqlite'";
        yield return "database: './target/sqlite-dev-db.sql'";
    }

    public override IEnumerable<EnvironmentVariableRequest> GetEnvironmentVariableRequests()
    {
        yield break;
    }

    public override IEnumerable<NpmPackageDependency> GetPackageDependencies()
    {
        yield return new NpmPackageDependency("sqlite3", "^5.0.2");
    }

    public override bool TryGetColumnType(string typeName, out (string Type, IEnumerable<string> AdditionalOptions) columnType)
    {
        columnType = default;
        return false;
    }
}