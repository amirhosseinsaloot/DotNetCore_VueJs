using Infrastructure.Services.Emails.Models;

namespace Infrastructure.Services.Emails.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken);

    Task SendEmailAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken);
}
