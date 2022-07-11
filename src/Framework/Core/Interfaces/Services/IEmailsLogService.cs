using Domain.Entities.Logging;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services;

public interface IEmailsLogService
{
    public Task SaveLogAsync(EmailsLog emailsLog, List<IFormFile>? attachments, CancellationToken cancellationToken);
}
