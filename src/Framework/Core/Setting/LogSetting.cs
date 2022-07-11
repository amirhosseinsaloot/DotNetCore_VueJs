using Microsoft.Extensions.Options;

namespace Domain.Setting;

public sealed record class LogSetting
{
    public string TableName { get; init; } = "SysError";

    public bool AutoCreateSqlTable { get; init; } = true;

    public LogLevelSerilog MinimumLevelSerilog { get; init; } = LogLevelSerilog.Error;
}

public class LogSettingValidation : IValidateOptions<LogSetting>
{
    public ValidateOptionsResult Validate(string name, LogSetting options)
    {
        if (string.IsNullOrEmpty(options.TableName))
        {
            return ValidateOptionsResult.Fail($"{nameof(LogSetting.TableName)} can not be empty.");
        }

        return ValidateOptionsResult.Success;
    }
}
