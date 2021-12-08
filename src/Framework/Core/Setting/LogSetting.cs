namespace Core.Setting;

public sealed record class LogSetting
{
    public string TableName { get; init; }

    public bool AutoCreateSqlTable { get; init; }

    public LogLevelSerilog MinimumLevelSerilog { get; init; }
}
