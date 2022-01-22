namespace Service.Emails.Models;

public record class EmailRequest
{
    public string ToEmail { get; init; } = default!;

    public string Subject { get; init; } = default!;

    public string Body { get; init; } = default!;

    public List<IFormFile>? Attachments { get; init; }
}
