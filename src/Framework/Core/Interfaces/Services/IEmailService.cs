using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string body, string subject, string toEmail, List<IFormFile>? attachments, CancellationToken cancellationToken);

    Task SendEmailAsync(string body, string subject, int toUserId, List<IFormFile>? attachments, CancellationToken cancellationToken);
}
