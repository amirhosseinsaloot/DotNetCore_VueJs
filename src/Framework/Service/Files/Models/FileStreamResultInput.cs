namespace Service.Files.Models;

public class FileStreamResultInput
{
    public Stream FileStream { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public string FileDownloadName { get; set; } = null!;
}
