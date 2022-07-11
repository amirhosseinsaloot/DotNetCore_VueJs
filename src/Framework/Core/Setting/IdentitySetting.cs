using Microsoft.Extensions.Options;

namespace Domain.Setting;

public sealed record class IdentitySetting
{
    public bool PasswordRequireDigit { get; init; } = true;

    public int PasswordRequiredLength { get; init; } = 6;

    public bool PasswordRequireNonAlphanumeric { get; init; } = false;

    public bool PasswordRequireUppercase { get; init; } = false;

    public bool PasswordRequireLowercase { get; init; } = false;

    public bool RequireUniqueEmail { get; init; } = true;
}

public class IdentitySettingValidation : IValidateOptions<IdentitySetting>
{
    public ValidateOptionsResult Validate(string name, IdentitySetting options)
    {
        if (options.PasswordRequiredLength < 6)
        {
            return ValidateOptionsResult.Fail($"{nameof(IdentitySetting.PasswordRequiredLength)} can not be less than 6.");
        }

        return ValidateOptionsResult.Success;
    }
}