﻿using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
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
        yield return "host: configService.get('DB_HOST')";
        yield return "username: configService.get('DB_USERNAME')";
        yield return "password: configService.get('DB_PASSWORD')";
        yield return "database: configService.get('DB_NAME')";
        yield return "extra: { trustServerCertificate: true }";
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
        yield return new NpmPackageDependency("mssql", "^7.3.0");
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