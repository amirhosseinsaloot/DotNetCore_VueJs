namespace Core.Setting;

public sealed record class ApplicationSettings
{
    public JwtSetting? JwtSetting { get; init; }

    public IdentitySetting? IdentitySetting { get; init; }

    public DatabaseSetting? DatabaseSetting { get; init; }

    public LogSetting? LogSetting { get; init; }

    public MailSetting? MailSetting { get; init; }
}
