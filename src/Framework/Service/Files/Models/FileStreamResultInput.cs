namespace Services.Files;

public class FileStreamResultInput
{
    public Stream FileStream { get; set; }

    public string ContentType { get; set; }

    public string FileDownloadName { get; set; }
}
