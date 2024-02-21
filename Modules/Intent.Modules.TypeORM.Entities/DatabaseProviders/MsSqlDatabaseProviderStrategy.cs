using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.NestJS.Core.Events;

namespace Intent.Modules.TypeORM.Entities.DatabaseProviders;

internal class MsSqlDatabaseProviderStrategy : OrmDatabaseProviderStrategyBase
{
    public MsSqlDatabaseProviderStrategy(IOutputTarget outputTarget) : base(outputTarget)
    {
    }

    public override IEnumerable<string> GetConfigurationOptions()
    {
        yield return "type: 'mssql'";
        yield return "host: get('DB_HOST').asString()";
        yield return "username: get('DB_USERNAME').asString()";
        yield return "password: get('DB_PASSWORD').asString()";
        yield return "database: get('DB_NAME').asString()";
        yield return "extra: { trustServerCertificate: true }";
    }

    public override bool TryGetColumnLength(AttributeModel attribute, out string lengthOptionValue)
    {
        if (!attribute.HasTextConstraints())
        {
            lengthOptionValue = default;
            return false;
        }

        // If length is unspecified, TypeORM regards it as 255.
        var maxLength = attribute.GetTextConstraints().MaxLength();
        lengthOptionValue = maxLength.HasValue
            ? maxLength.Value.ToString("D")
            : "'max'";
        return true;

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
        yield return new NpmPackageDependency("mssql", "^10.0.2");
    }

    public override bool TryGetColumnType(string typeName, out (string Type, IEnumerable<string> AdditionalOptions) columnType)
    {
        columnType = typeName switch
        {
            "binary" => ("varbinary", Enumerable.Empty<string>()),
            "bool" => ("bit", Enumerable.Empty<string>()),
            "byte" => ("tinyint", Enumerable.Empty<string>()),
            "char" => ("char", new[] { "length: 1" }),
            "date" => ("date", Enumerable.Empty<string>()),
            "datetime" => ("datetime2", Enumerable.Empty<string>()),
            "datetimeoffset" => ("datetimeoffset", Enumerable.Empty<string>()),
            "decimal" => ("decimal", new[] { "precision: 18", "scale: 2" }),
            "double" => ("float", Enumerable.Empty<string>()),
            "float" => ("float", Enumerable.Empty<string>()),
            "guid" => ("uniqueidentifier", Enumerable.Empty<string>()),
            "int" => ("int", Enumerable.Empty<string>()),
            "long" => ("bigint", Enumerable.Empty<string>()),
            "short" => ("smallint", Enumerable.Empty<string>()),
            "string" => ("varchar", Enumerable.Empty<string>()),
            _ => default
        };

        return columnType != default;
    }
}