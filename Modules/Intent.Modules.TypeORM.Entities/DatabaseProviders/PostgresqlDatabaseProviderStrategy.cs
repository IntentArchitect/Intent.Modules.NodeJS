using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;

namespace Intent.Modules.TypeORM.Entities.DatabaseProviders;

internal class PostgresqlDatabaseProviderStrategy : OrmDatabaseProviderStrategyBase
{
    public PostgresqlDatabaseProviderStrategy(IOutputTarget outputTarget) : base(outputTarget)
    {
    }

    public override IEnumerable<string> GetConfigurationOptions()
    {
        yield return "type: 'postgres'";
        yield return "host: get('DB_HOST').asString()";
        yield return "username: get('DB_USERNAME').asString()";
        yield return "password: get('DB_PASSWORD').asString()";
        yield return "database: get('DB_NAME').asString()";
    }

    public override bool TryGetColumnLength(AttributeModel attribute, out string lengthOptionValue)
    {
        var maxLength = attribute.GetTextConstraints()?.MaxLength();
        if (maxLength.HasValue)
        {
            lengthOptionValue = maxLength.Value.ToString("D");
            return true;
        }

        lengthOptionValue = default;
        return false;
    }

    public override IEnumerable<EnvironmentVariableRequest> GetEnvironmentVariableRequests()
    {
        yield return new EnvironmentVariableRequest("DB_HOST", "localhost");
        yield return new EnvironmentVariableRequest("DB_USERNAME", "");
        yield return new EnvironmentVariableRequest("DB_PASSWORD", "");
        yield return new EnvironmentVariableRequest("DB_NAME", OutputTarget.ApplicationName());
    }

    public override IEnumerable<NpmPackageDependency> GetPackageDependencies()
    {
        yield return new NpmPackageDependency("pg", "^8.11.3");
    }

    public override bool TryGetColumnType(string typeName, out (string Type, IEnumerable<string> AdditionalOptions) columnType)
    {
        columnType = typeName switch
        {
            "binary" => ("bytea", []),
            "bool" => ("boolean", []),
            "byte" => ("bytea", []),
            "char" => ("character", []),
            "date" => ("date", []),
            "datetime" => ("timestamp", []),
            "datetimeoffset" => ("timestamp with time zone", []),
            "decimal" => ("decimal", ["precision: 18", "scale: 2"]),
            "double" => ("double precision", []),
            "float" => ("real", []),
            "guid" => ("uuid", []),
            "int" => ("integer", []),
            "long" => ("bigint", []),
            "short" => ("smallint", []),
            "string" => ("character varying", []),
            _ => default
        };

        return columnType != default;
    }
}