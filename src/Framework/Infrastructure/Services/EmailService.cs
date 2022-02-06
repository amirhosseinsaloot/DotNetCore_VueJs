using Core.Entities.Identity;
using Core.Entities.Logging;
using Core.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly MailSetting _mailSetting;

    private readonly IEmailsLogService _emailsLogService;

    private readonly IDataProvider<User> _userDataProvider;

    public EmailService(IOptions<ApplicationSettings> settings, IEmailsLogService emailsLogService, IDataProvider<User> userDataProvider)
    {
        _mailSetting = settings.Value.MailSetting;
        _emailsLogService = emailsLogService;
        _userDataProvider = userDataProvider;
    }

    public async Task SendEmailAsync(string body, string subject, string toEmail, List<IFormFile>? attachments, CancellationToken cancellationToken)
    {
        // Filling BodyBuilder instance
        var builder = new BodyBuilder();
        if (attachments != null)
        {
            MemoryStream memoryStream;
            foreach (var file in attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        memoryStream = ms;
                    }
                    await builder.Attachments.AddAsync(file.FileName, memoryStream, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = body;

        // Filling MimeMessage instance
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSetting.EmailAddress),
            Subject = subject,
            Body = builder.ToMessageBody(),
        };
        email.To.Add(MailboxAddress.Parse(toEmail));

        // Sending Process
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSetting.SmtpServer, _mailSetting.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSetting.EmailAddress, _mailSetting.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        // Save email in database
        var emailsLog = new EmailsLog
        {
            ToEmail = toEmail,
            Subject = subject,
            Body = body,
            ToUserId = null,
        };
        await _emailsLogService.SaveLogAsync(emailsLog, attachments, cancellationToken);
    }

    public async Task SendEmailAsync(string body, string subject, int toUserId, List<IFormFile>? attachments, CancellationToken cancellationToken)
    {
        var user = await _userDataProvider.GetByIdAsync(toUserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("User not found for sending email.");
        }

        // Filling BodyBuilder instance
        var builder = new BodyBuilder();
        if (attachments != null)
        {
            MemoryStream memoryStream;
            foreach (var file in attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        memoryStream = ms;
                    }
                    await builder.Attachments.AddAsync(file.FileName, memoryStream, ContentType.Parse(file.ContentType));
                }
            }
        }

        builder.HtmlBody = body;

        // Filling MimeMessage instance
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSetting.EmailAddress),
            Subject = subject,
            Body = builder.ToMessageBody(),
        };

        email.To.Add(MailboxAddress.Parse(user.Email));

        // Sending Process
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSetting.SmtpServer, _mailSetting.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSetting.EmailAddress, _mailSetting.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        // Save email in database
        var emailsLog = new EmailsLog
        {
            ToEmail = null,
            Subject = subject,
            Body = body,
            ToUserId = toUserId,
        };
        await _emailsLogService.SaveLogAsync(emailsLog, attachments, cancellationToken);
    }
}
