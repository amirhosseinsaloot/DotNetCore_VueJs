using Service.Emails.Models;

namespace Service.Emails.Services;

public interface IEmailsLogService
{
    public Task SaveLogAsync(EmailRequest emailRequest, CancellationToken cancellationToken);

    public Task SaveLogAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken);
}
