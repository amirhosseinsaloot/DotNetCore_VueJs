namespace Service.Emails.Models;

public record class EmailRequest
{
    public string ToEmail { get; init; } = null!;

    public string Subject { get; init; } = null!;

    public string Body { get; init; } = null!;

    public List<IFormFile>? Attachments { get; init; }
}
