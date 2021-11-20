namespace Services.Emails;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken);

    Task SendEmailAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken);
}
