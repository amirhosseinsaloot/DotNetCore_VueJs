using Microsoft.Extensions.Options;

namespace Core.Setting;

public sealed record class MailSetting
{
    public string EmailAddress { get; init; } = null!;

    public string DisplayName { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string SmtpServer { get; init; } = null!;

    public int Port { get; init; }
}

public class MailSettingValidation : IValidateOptions<MailSetting>
{
    public ValidateOptionsResult Validate(string name, MailSetting options)
    {
        if (string.IsNullOrEmpty(options.EmailAddress))
        {
            return ValidateOptionsResult.Fail($"{nameof(MailSetting.EmailAddress)} in mail setting not configured.");
        }

        if (string.IsNullOrEmpty(options.DisplayName))
        {
            return ValidateOptionsResult.Fail($"{nameof(MailSetting.DisplayName)} in mail setting not configured.");
        }

        if (string.IsNullOrEmpty(options.Password))
        {
            return ValidateOptionsResult.Fail($"{nameof(MailSetting.Password)} in mail setting not configured.");
        }

        if (string.IsNullOrEmpty(options.SmtpServer))
        {
            return ValidateOptionsResult.Fail($"{nameof(MailSetting.SmtpServer)} in mail setting not configured.");
        }

        if (options.Port <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(MailSetting.Port)} in mail setting not valid.");
        }

        return ValidateOptionsResult.Success;
    }
}