using Data.Entities.Logging;
using Service.Emails.Models;
using Service.Files.Services;

namespace Service.Emails.Services;

public class EmailsLogService : IEmailsLogService
{
    #region Fields

    private readonly IDataProvider<EmailsLog> _emailsLogDataProvider;

    private readonly IFileService _fileService;

    #endregion

    #region Properties

    private string FilesDescription => "Email Attachment";

    #endregion

    #region Ctor

    public EmailsLogService(IDataProvider<EmailsLog> emailsLogDataProvider, IFileService fileService)
    {
        _emailsLogDataProvider = emailsLogDataProvider;
        _fileService = fileService;
    }

    #endregion

    #region Methods

    public async Task SaveLogAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
    {
        var fileModelIds = await _fileService.StoreFilesAsync(emailRequest.Attachments, FilesDescription, cancellationToken);
        var emailsLog = new EmailsLog
        {
            ToEmail = emailRequest.ToEmail,
            Subject = emailRequest.Subject,
            Body = emailRequest.Body,
            ToUserId = null,
        };

        var emailsLogFileModels = new List<EmailsLogFileModel>();

        foreach (var fileModelId in fileModelIds)
        {
            emailsLogFileModels.Add(new EmailsLogFileModel { FileModelId = fileModelId, EmailsLog = emailsLog });
        }

        emailsLog.EmailsLogFileModels = emailsLogFileModels;

        await _emailsLogDataProvider.AddAsync(emailsLog, cancellationToken);
    }

    public async Task SaveLogAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken)
    {
        var fileModelIds = await _fileService.StoreFilesAsync(emailRequestToUser.Attachments, FilesDescription, cancellationToken);
        var emailsLog = new EmailsLog
        {
            ToEmail = null,
            Subject = emailRequestToUser.Subject,
            Body = emailRequestToUser.Body,
            ToUserId = emailRequestToUser.UserId,
        };

        var emailsLogFileModels = new List<EmailsLogFileModel>();

        foreach (var fileModelId in fileModelIds)
        {
            emailsLogFileModels.Add(new EmailsLogFileModel { FileModelId = fileModelId, EmailsLog = emailsLog });
        }

        emailsLog.EmailsLogFileModels = emailsLogFileModels;

        await _emailsLogDataProvider.AddAsync(emailsLog, cancellationToken);
    }

    #endregion
}
