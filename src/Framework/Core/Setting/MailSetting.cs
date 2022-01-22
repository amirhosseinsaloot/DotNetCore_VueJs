namespace Core.Setting;

public sealed record class MailSetting
{
    public string EmailAddress { get; init; } = default!;

    public string DisplayName { get; init; } = default!;

    public string Password { get; init; } = default!;

    public string SmtpServer { get; init; } = default!;

    public int Port { get; init; }
}
