using Microsoft.Extensions.Options;

namespace Core.Setting;

public sealed record class ApplicationSettings
{
    public JwtSetting JwtSetting { get; init; } = null!;

    public IdentitySetting IdentitySetting { get; init; } = null!;

    public DatabaseSetting DatabaseSetting { get; init; } = null!;

    public LogSetting LogSetting { get; init; } = null!;

    public MailSetting MailSetting { get; init; } = null!;
}

public class ApplicationSettingsValidation : IValidateOptions<ApplicationSettings>
{
    public ValidateOptionsResult Validate(string name, ApplicationSettings options)
    {
        return ValidateOptionsResult.Success;
    }
}
