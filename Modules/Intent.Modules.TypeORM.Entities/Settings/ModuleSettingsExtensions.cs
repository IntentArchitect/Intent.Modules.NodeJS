using System;
using Intent.Configuration;
using Intent.Engine;
using Intent.Modules.Common.Templates;
using Intent.Modules.Metadata.RDBMS.Settings;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Settings.ModuleSettingsExtensions", Version = "1.0")]

namespace Intent.Modules.TypeORM.Entities.Settings
{

    public static class DatabaseSettingsExtensions
    {
        public static DatabaseProviderOptions DatabaseProvider(this DatabaseSettings groupSettings) => new DatabaseProviderOptions(groupSettings.GetSetting("9bcab235-06d6-46dc-bc8d-b49483c6914d")?.Value);

        public class DatabaseProviderOptions
        {
            public readonly string Value;

            public DatabaseProviderOptions(string value)
            {
                Value = value;
            }

            public DatabaseProviderOptionsEnum AsEnum()
            {
                return Value switch
                {
                    "mssql" => DatabaseProviderOptionsEnum.Mssql,
                    "postgresql" => DatabaseProviderOptionsEnum.Postgresql,
                    "sqlite" => DatabaseProviderOptionsEnum.Sqlite,
                    _ => throw new ArgumentOutOfRangeException(nameof(Value), $"{Value} is out of range")
                };
            }

            public bool IsMssql()
            {
                return Value == "mssql";
            }

            public bool IsPostgresql()
            {
                return Value == "postgresql";
            }

            public bool IsSqlite()
            {
                return Value == "sqlite";
            }
        }

        public enum DatabaseProviderOptionsEnum
        {
            Mssql,
            Postgresql,
            Sqlite,
        }
    }
}