namespace Core.Setting;

public sealed record class JwtSetting
{
    public string SecretKey { get; init; }

    public string EncryptKey { get; init; }

    public string Issuer { get; init; }

    public string Audience { get; init; }

    public int NotBeforeMinutes { get; init; }

    public int AccessTokenExpirationDays { get; init; }

    public int RefreshTokenExpirationDays { get; init; }
}
