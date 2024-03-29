﻿using System;
using System.Collections.Generic;
using Intent.Engine;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.TypeScript.Templates;
using Intent.Modules.Metadata.RDBMS.Settings;
using Intent.Modules.NestJS.Core.Events;
using Intent.Modules.TypeORM.Entities.Settings;

namespace Intent.Modules.TypeORM.Entities.DatabaseProviders;

internal interface IOrmDatabaseProviderStrategy
{
    bool TryGetColumnType(AttributeModel attribute, out (string Type, IEnumerable<string> AdditionalOptions) columnType);
    bool TryGetColumnType(string typeName, out (string Type, IEnumerable<string> AdditionalOptions) columnType);
    bool TryGetColumnLength(AttributeModel attribute, out string lengthOptionValue);
    IEnumerable<EnvironmentVariableRequest> GetEnvironmentVariableRequests();
    IEnumerable<string> GetConfigurationOptions();
    IEnumerable<NpmPackageDependency> GetPackageDependencies();

    static IOrmDatabaseProviderStrategy Resolve(IOutputTarget outputTarget)
    {
        var databaseProvider = outputTarget.ExecutionContext.Settings.GetDatabaseSettings()?.DatabaseProvider()?.AsEnum();

        return databaseProvider switch
        {
            DatabaseSettingsExtensions.DatabaseProviderOptionsEnum.Mssql => new MsSqlDatabaseProviderStrategy(outputTarget),
            DatabaseSettingsExtensions.DatabaseProviderOptionsEnum.Sqlite => new SqLiteDatabaseProviderStrategy(outputTarget),
            DatabaseSettingsExtensions.DatabaseProviderOptionsEnum.Postgresql => new PostgresqlDatabaseProviderStrategy(outputTarget),
            null => throw new Exception("The database provider application setting is unset."),
            _ => throw new Exception($"No database provider strategy available for {databaseProvider}")
        };
    }
}