namespace Service.Emails.Models;

public record class EmailRequestToUser
{
    public int UserId { get; set; }

    public string Subject { get; init; }

    public string Body { get; init; }

    public List<IFormFile> Attachments { get; init; }
}
