using Domain.Setting;
using Domain.Utilities;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Api.Extensions;

public static class SerilogExtensions
{
    public static void Register(ApplicationSettings? applicationSettings)
    {
        if (applicationSettings is null)
        {
            throw new ArgumentNullException(nameof(applicationSettings));
        }

        var databaseSetting = applicationSettings.DatabaseSetting;
        var logSetting = applicationSettings.LogSetting;

        switch (databaseSetting.DatabaseProvider)
        {
            case DatabaseProvider.Postgres:
                PostgresRegistration(logSetting, databaseSetting.ConnectionStrings.Postgres!);
                break;

            case DatabaseProvider.SqlServer:
                SqlServerRegistration(logSetting, databaseSetting.ConnectionStrings.SqlServer!);
                break;

            default:
                throw new Exception("Database provider not found.");
        }
    }

    private static void PostgresRegistration(LogSetting logSetting, string connectionString)
    {
        Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .PostgreSQL(
                                connectionString: connectionString,
                                tableName: logSetting.TableName.ToSnakeCase(),
                                restrictedToMinimumLevel: (LogEventLevel)logSetting.MinimumLevelSerilog,
                                needAutoCreateTable: logSetting.AutoCreateSqlTable
                                )
                    .CreateLogger();
    }

    private static void SqlServerRegistration(LogSetting logSetting, string connectionString)
    {

        var sqlServerSinkOptions = new MSSqlServerSinkOptions
        {
            TableName = logSetting.TableName,
            AutoCreateSqlTable = logSetting.AutoCreateSqlTable
        };

        Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .MSSqlServer(
                                connectionString: connectionString,
                                sinkOptions: sqlServerSinkOptions,
                                restrictedToMinimumLevel: (LogEventLevel)logSetting.MinimumLevelSerilog
                                )
                    .CreateLogger();
    }
}
