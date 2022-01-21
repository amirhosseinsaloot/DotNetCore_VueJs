namespace Core.Setting;

public sealed record class IdentitySetting
{
    public bool PasswordRequireDigit { get; init; } = true;

    public int PasswordRequiredLength { get; init; } = 6;

    public bool PasswordRequireNonAlphanumeric { get; init; } = false;

    public bool PasswordRequireUppercase { get; init; } = false;

    public bool PasswordRequireLowercase { get; init; } = false;

    public bool RequireUniqueEmail { get; init; } = true;
}
