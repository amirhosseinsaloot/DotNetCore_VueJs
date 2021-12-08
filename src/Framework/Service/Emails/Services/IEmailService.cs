using Service.Emails.Models;

namespace Service.Emails.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken);

    Task SendEmailAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken);
}
