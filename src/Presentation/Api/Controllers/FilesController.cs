using Core.Enums;
using Core.Response;
using Core.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Files.Services;

namespace Api.Controllers;

public class FilesController : BaseController
{
    #region Fields

    private readonly IFileService _fileService;

    #endregion Fields

    #region Ctor

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    #endregion Ctor

    #region Actions

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

    #endregion Actions
}
