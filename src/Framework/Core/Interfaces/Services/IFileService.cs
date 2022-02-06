using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Interfaces.Services;

public interface IFileService
{
    Task<int> StoreFileAsync(IFormFile formFile, string description, CancellationToken cancellationToken);

    Task<List<int>> StoreFilesAsync(List<IFormFile> formFiles, string description, CancellationToken cancellationToken);

    Task<FileStreamResult> GetFileByIdAsync(int id, CancellationToken cancellationToken);

    Task DeleteFileAsync(int id, CancellationToken cancellationToken);
}
