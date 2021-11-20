using Microsoft.AspNetCore.Http;

namespace Services.Emails;

public record class EmailRequest
{
    public string ToEmail { get; init; }

    public string Subject { get; init; }

    public string Body { get; init; }

    public List<IFormFile> Attachments { get; init; }
}
