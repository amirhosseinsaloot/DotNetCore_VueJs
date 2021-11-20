namespace Core.Setting;

public sealed record class MailSetting
{
    public string EmailAddress { get; init; }

    public string DisplayName { get; init; }

    public string Password { get; init; }

    public string SmtpServer { get; init; }

    public int Port { get; init; }
}
