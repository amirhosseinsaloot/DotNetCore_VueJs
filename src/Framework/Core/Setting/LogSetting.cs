namespace Core.Setting;

public sealed record class LogSetting
{
    public string TableName { get; init; } = "SysError";

    public bool AutoCreateSqlTable { get; init; } = true;

    public LogLevelSerilog MinimumLevelSerilog { get; init; } = LogLevelSerilog.Error;
}
