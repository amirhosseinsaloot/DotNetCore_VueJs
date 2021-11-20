using Microsoft.AspNetCore.Http;

namespace Services.Files.Services;

public interface IFileService
{
    Task<int> StoreFileAsync(IFormFile formFile, string description, CancellationToken cancellationToken);

    Task<List<int>> StoreFilesAsync(List<IFormFile> formFiles, string description, CancellationToken cancellationToken);

    Task<FileStreamResultInput> GetFileByIdAsync(int id, CancellationToken cancellationToken);

    Task DeleteFileAsync(int id, CancellationToken cancellationToken);
}
