namespace Infrastructure.Services.Emails.Models;

public record class EmailRequestToUser
{
    public int UserId { get; set; }

    public string Subject { get; init; } = null!;

    public string Body { get; init; } = null!;

    public List<IFormFile>? Attachments { get; init; }
}
