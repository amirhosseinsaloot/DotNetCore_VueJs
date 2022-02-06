using Core.Interfaces.Services;

namespace Api.Controllers;

public class FilesController : BaseController
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost, Authorize(Roles = ApplicationRoles.TeamMember_ToTheTop)]
    public async Task<ApiResponse<object>> StoreFile(IFormFile formFile, string description, CancellationToken cancellationToken)
    {
        return new ApiResponse<object>(true, ApiResultBodyCode.Success, new { Id = await _fileService.StoreFileAsync(formFile, description, cancellationToken) });
    }

    [HttpGet("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamMember_ToTheTop)]
    public async Task<ActionResult> GetFileById(int id, CancellationToken cancellationToken)
    {
        var file = await _fileService.GetFileByIdAsync(id, cancellationToken);
        return File(file.FileStream, file.ContentType, file.FileDownloadName);
    }

    [HttpDelete("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamMember_ToTheTop)]
    public async Task<ApiResponse> DeleteFile(int id, CancellationToken cancellationToken)
    {
        await _fileService.DeleteFileAsync(id, cancellationToken);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }
}