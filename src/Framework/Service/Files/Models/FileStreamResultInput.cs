namespace Service.Files.Models;

public class FileStreamResultInput
{
    public Stream FileStream { get; set; } = default!;

    public string ContentType { get; set; } = default!;

    public string FileDownloadName { get; set; } = default!;
}
