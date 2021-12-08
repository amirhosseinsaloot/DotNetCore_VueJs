namespace Service.Emails.Models;

public record class EmailRequest
{
    public string ToEmail { get; init; }

    public string Subject { get; init; }

    public string Body { get; init; }

    public List<IFormFile> Attachments { get; init; }
}
