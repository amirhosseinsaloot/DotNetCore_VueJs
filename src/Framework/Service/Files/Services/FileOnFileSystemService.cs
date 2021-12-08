using Data.Entities.Files;
using Service.Files.Models;

namespace Service.Files.Services;

public class FileOnFileSystemService : IFileService
{
    #region Fields

    private readonly IDataProvider<FileOnFileSystem> _fileOnFileSystemDataProvider;

    #endregion

    #region Ctor

    public FileOnFileSystemService(IDataProvider<FileOnFileSystem> fileOnFileSystemDataProvider)
    {
        _fileOnFileSystemDataProvider = fileOnFileSystemDataProvider;
    }

    #endregion

    #region Methods

    public async Task<int> StoreFileAsync(IFormFile formFile, string description, CancellationToken cancellationToken)
    {
        if (formFile is null)
        {
            throw new BadRequestException("File is empty object");
        }

        var basePath = Path.Combine("Resources", "Images");
        var basePathExists = Directory.Exists(basePath);
        if (basePathExists is false)
        {
            Directory.CreateDirectory(basePath);
        }
        var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
        var filePath = Path.Combine(basePath, formFile.FileName);
        var extension = Path.GetExtension(formFile.FileName);
        if (File.Exists(filePath))
        {
            fileName = $"{fileName}_{DateTime.UtcNow.Ticks}";
            filePath = Path.Combine(basePath, fileName);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        var fileModel = new FileOnFileSystem
        {
            Name = fileName,
            FileType = formFile.ContentType,
            Extension = extension,
            Description = description,
            FilePath = filePath
        };

        return await _fileOnFileSystemDataProvider.AddAsync(fileModel, cancellationToken);
    }

    public async Task<List<int>> StoreFilesAsync(List<IFormFile> formFiles, string description, CancellationToken cancellationToken)
    {
        if (formFiles is null)
        {
            throw new BadRequestException("Files is empty object");
        }

        var fileModels = new List<FileOnFileSystem>();

        foreach (var formFile in formFiles)
        {
            var basePath = Path.Combine("Resources", "Images");
            var basePathExists = Directory.Exists(basePath);
            if (basePathExists is false)
            {
                Directory.CreateDirectory(basePath);
            }

            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            var filePath = Path.Combine(basePath, formFile.FileName);
            var extension = Path.GetExtension(formFile.FileName);
            if (File.Exists(filePath))
            {
                fileName = $"{fileName}_{DateTime.UtcNow.Ticks}";
                filePath = Path.Combine(basePath, fileName);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var fileModel = new FileOnFileSystem
            {
                Name = fileName,
                FileType = formFile.ContentType,
                Extension = extension,
                Description = description,
                FilePath = filePath
            };

            fileModels.Add(fileModel);
        }

        return await _fileOnFileSystemDataProvider.AddRangeAsync(fileModels, cancellationToken);
    }

    public async Task<FileStreamResultInput> GetFileByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _fileOnFileSystemDataProvider.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException("File not found");
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(entity.FilePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        var fileStreamResultInput = new FileStreamResultInput
        {
            FileStream = memory,
            ContentType = entity.FileType,
            FileDownloadName = entity.Name + entity.Extension
        };
        return fileStreamResultInput;
    }

    public async Task DeleteFileAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _fileOnFileSystemDataProvider.GetByIdAsync(id, cancellationToken);

        if (File.Exists(entity.FilePath))
        {
            File.Delete(entity.FilePath);
        }
        await _fileOnFileSystemDataProvider.RemoveAsync(id, cancellationToken);
    }

    #endregion
}
