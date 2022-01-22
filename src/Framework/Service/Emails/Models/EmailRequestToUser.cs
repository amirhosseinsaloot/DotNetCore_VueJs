namespace Service.Emails.Models;

public record class EmailRequestToUser
{
    public int UserId { get; set; }

    public string Subject { get; init; } = default!;

    public string Body { get; init; } = default!;

    public List<IFormFile>? Attachments { get; init; }
}
