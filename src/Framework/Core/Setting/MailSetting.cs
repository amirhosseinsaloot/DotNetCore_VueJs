namespace Core.Setting;

public sealed record class MailSetting
{
    public string EmailAddress { get; init; } = null!;

    public string DisplayName { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string SmtpServer { get; init; } = null!;

    public int Port { get; init; }
}
