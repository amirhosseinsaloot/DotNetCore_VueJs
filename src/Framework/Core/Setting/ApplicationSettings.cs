namespace Core.Setting;

public sealed record class ApplicationSettings
{
    public JwtSetting JwtSetting { get; init; } = default!;

    public IdentitySetting IdentitySetting { get; init; } = default!;

    public DatabaseSetting DatabaseSetting { get; init; } = default!;

    public LogSetting LogSetting { get; init; } = default!;

    public MailSetting MailSetting { get; init; } = default!;
}
