using Microsoft.Extensions.Options;

namespace Domain.Setting;

public sealed record class JwtSetting
{
    public string SecretKey { get; init; } = "LongerThan-16Char-SecretKey";

    public string EncryptKey { get; init; } = "16CharEncryptKey";

    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public int NotBeforeMinutes { get; init; } = 0;

    public int AccessTokenExpirationDays { get; init; } = 1;

    public int RefreshTokenExpirationDays { get; init; } = 7;
}

public class JwtSettingValidation : IValidateOptions<JwtSetting>
{
    public ValidateOptionsResult Validate(string name, JwtSetting options)
    {
        if (options.NotBeforeMinutes < 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(JwtSetting.NotBeforeMinutes)} can not be less than 0.");

        }
        if (options.AccessTokenExpirationDays < 1)
        {
            return ValidateOptionsResult.Fail($"{nameof(JwtSetting.AccessTokenExpirationDays)} can not be less tha 1.");
        }

        if (options.RefreshTokenExpirationDays < 1)
        {
            return ValidateOptionsResult.Fail($"{nameof(JwtSetting.RefreshTokenExpirationDays)} can not be less tha 1.");
        }

        return ValidateOptionsResult.Success;
    }
}