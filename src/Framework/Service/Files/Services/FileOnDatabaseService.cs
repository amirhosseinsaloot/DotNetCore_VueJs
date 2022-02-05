using Data.Entities.Files;
using Service.Files.Models;

namespace Service.Files.Services;

public class FileOnDatabaseService : IFileService
{
    private readonly IDataProvider<FileOnDatabase> _fileOnDatabaseDataProvider;

    public FileOnDatabaseService(IDataProvider<FileOnDatabase> fileOnDatabaseDataProvider)
    {
        _fileOnDatabaseDataProvider = fileOnDatabaseDataProvider;
    }

    public async Task<int> StoreFileAsync(IFormFile formFile, string description, CancellationToken cancellationToken)
    {
        if (formFile is null)
        {
            throw new BadRequestException("File is empty object");
        }
        var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
        var extension = Path.GetExtension(formFile.FileName);
        var fileModel = new FileOnDatabase
        {
            FileType = formFile.ContentType,
            Extension = extension,
            Name = fileName,
            Description = description
        };

        using (var dataStream = new MemoryStream())
        {
            await formFile.CopyToAsync(dataStream);
            fileModel.Data = dataStream.ToArray();
        }

        return await _fileOnDatabaseDataProvider.AddAsync(fileModel, cancellationToken);
    }

    public async Task<List<int>> StoreFilesAsync(List<IFormFile> formFiles, string description, CancellationToken cancellationToken)
    {

        if (formFiles is null)
        {
            throw new BadRequestException("Files is empty object");
        }
        var fileModels = new List<FileOnDatabase>();

        foreach (var formFile in formFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            var extension = Path.GetExtension(formFile.FileName);
            var fileModel = new FileOnDatabase
            {
                FileType = formFile.ContentType,
                Extension = extension,
                Name = fileName,
                Description = description
            };

            using (var dataStream = new MemoryStream())
            {
                await formFile.CopyToAsync(dataStream);
                fileModel.Data = dataStream.ToArray();
            }

            fileModels.Add(fileModel);
        }

        return await _fileOnDatabaseDataProvider.AddRangeAsync(fileModels, cancellationToken);
    }

    public async Task<FileStreamResultInput> GetFileByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _fileOnDatabaseDataProvider.GetByIdAsync(id, cancellationToken);
        var fileStreamResultInput = new FileStreamResultInput
        {
            FileStream = new MemoryStream(entity.Data!),
            ContentType = entity.FileType,
            FileDownloadName = entity.Name + entity.Extension
        };

        return fileStreamResultInput;
    }

    public async Task DeleteFileAsync(int id, CancellationToken cancellationToken)
    {
        await _fileOnDatabaseDataProvider.RemoveAsync(id, cancellationToken);
    }
}
