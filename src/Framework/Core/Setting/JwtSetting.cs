namespace Core.Setting;

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
