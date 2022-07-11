using Domain.Entities.Logging;
using Domain.Interfaces.Services;

namespace Infrastructure.Services.Emails;

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

    public async Task SaveLogAsync(EmailsLog emailsLog, List<IFormFile>? attachments, CancellationToken cancellationToken)
    {
        if (attachments is not null)
        {
            var fileModelIds = await _fileService.StoreFilesAsync(attachments, FilesDescription, cancellationToken);
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
