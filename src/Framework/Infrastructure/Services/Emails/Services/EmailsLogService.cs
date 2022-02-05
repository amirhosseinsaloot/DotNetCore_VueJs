using Core.Entities.Logging;
using Infrastructure.Services.Emails.Models;
using Infrastructure.Services.Files.Services;

namespace Infrastructure.Services.Emails.Services;

public class EmailsLogService : IEmailsLogService
{
    private readonly IDataProvider<EmailsLog> _emailsLogDataProvider;

    private readonly IFileService _fileService;

    private string FilesDescription => "Email Attachment";

    public EmailsLogService(IDataProvider<EmailsLog> emailsLogDataProvider, IFileService fileService)
    {
        _emailsLogDataProvider = emailsLogDataProvider;
        _fileService = fileService;
    }

    public async Task SaveLogAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
    {
        var emailsLog = new EmailsLog
        {
            ToEmail = emailRequest.ToEmail,
            Subject = emailRequest.Subject,
            Body = emailRequest.Body,
            ToUserId = null,
        };

        if (emailRequest.Attachments is not null)
        {
            var fileModelIds = await _fileService.StoreFilesAsync(emailRequest.Attachments, FilesDescription, cancellationToken);
            var emailsLogFileModels = new List<EmailsLogFileModel>();

            foreach (var fileModelId in fileModelIds)
            {
                emailsLogFileModels.Add(new EmailsLogFileModel { FileModelId = fileModelId, EmailsLog = emailsLog });
            }

            emailsLog.EmailsLogFileModels = emailsLogFileModels;
        }

        await _emailsLogDataProvider.AddAsync(emailsLog, cancellationToken);
    }

    public async Task SaveLogAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken)
    {
        var emailsLog = new EmailsLog
        {
            ToEmail = null,
            Subject = emailRequestToUser.Subject,
            Body = emailRequestToUser.Body,
            ToUserId = emailRequestToUser.UserId,
        };

        if (emailRequestToUser.Attachments is not null)
        {
            var fileModelIds = await _fileService.StoreFilesAsync(emailRequestToUser.Attachments, FilesDescription, cancellationToken);
            var emailsLogFileModels = new List<EmailsLogFileModel>();

            foreach (var fileModelId in fileModelIds)
            {
                emailsLogFileModels.Add(new EmailsLogFileModel { FileModelId = fileModelId, EmailsLog = emailsLog });
            }

            emailsLog.EmailsLogFileModels = emailsLogFileModels;
        }

        await _emailsLogDataProvider.AddAsync(emailsLog, cancellationToken);
    }
}
