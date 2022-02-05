using Core.Entities.Identity;
using Infrastructure.Services.Emails.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Emails.Services;

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

    public async Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
    {
        // Filling BodyBuilder instance
        var builder = new BodyBuilder();
        if (emailRequest.Attachments != null)
        {
            MemoryStream memoryStream;
            foreach (var file in emailRequest.Attachments)
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
        builder.HtmlBody = emailRequest.Body;

        // Filling MimeMessage instance
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSetting.EmailAddress),
            Subject = emailRequest.Subject,
            Body = builder.ToMessageBody(),
        };
        email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));

        // Sending Process
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSetting.SmtpServer, _mailSetting.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSetting.EmailAddress, _mailSetting.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        // Save email in database
        await _emailsLogService.SaveLogAsync(emailRequest, cancellationToken);
    }

    public async Task SendEmailAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken)
    {
        var user = await _userDataProvider.GetByIdAsync(emailRequestToUser.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("User not found for sending email.");
        }

        // Filling BodyBuilder instance
        var builder = new BodyBuilder();
        if (emailRequestToUser.Attachments != null)
        {
            MemoryStream memoryStream;
            foreach (var file in emailRequestToUser.Attachments)
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

        builder.HtmlBody = emailRequestToUser.Body;

        // Filling MimeMessage instance
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSetting.EmailAddress),
            Subject = emailRequestToUser.Subject,
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
        await _emailsLogService.SaveLogAsync(emailRequestToUser, cancellationToken);
    }
}
