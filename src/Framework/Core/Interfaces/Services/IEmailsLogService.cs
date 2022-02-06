using Core.Entities.Logging;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Services;

public interface IEmailsLogService
{
    public Task SaveLogAsync(EmailsLog emailsLog, List<IFormFile>? attachments, CancellationToken cancellationToken);
}
