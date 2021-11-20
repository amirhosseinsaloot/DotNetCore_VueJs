namespace Core.Setting;

public sealed record class IdentitySetting
{
    public bool PasswordRequireDigit { get; init; }

    public int PasswordRequiredLength { get; init; }

    public bool PasswordRequireNonAlphanumeric { get; init; }

    public bool PasswordRequireUppercase { get; init; }

    public bool PasswordRequireLowercase { get; init; }

    public bool RequireUniqueEmail { get; init; }
}
