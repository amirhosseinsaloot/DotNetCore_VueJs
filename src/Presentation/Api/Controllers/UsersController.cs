using Core.Enums;
using Core.Response;
using Core.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Domain;

namespace Api.Controllers;

public class UsersController : BaseController
{
    #region Fields

    private readonly UserService _userService;

    #endregion Fields

    #region Ctor

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    #endregion Ctor

    #region Actions

    [HttpGet, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<List<UserListViewModel>>> GetAllUsers()
    {
        var userList = await _userService.GetAllUsersAsync();
        return new ApiResponse<List<UserListViewModel>>(true, ApiResultBodyCode.Success, userList);
    }

    [HttpGet("{id:int:min(1)}"), Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<UserViewModel>> GetUserById(int id)
    {
        var userViewModel = await _userService.GetByIdAsync(id);
        return new ApiResponse<UserViewModel>(true, ApiResultBodyCode.Success, userViewModel);
    }

    [HttpPost, Authorize(Roles = ApplicationRoles.TeamAdmin_ToTheTop)]
    public async Task<ApiResponse<UserViewModel>> CreateUser(UserCreateViewModel userCreateViewModel)
    {
        var userViewModel = await _userService.CreateAsync(userCreateViewModel);
        return new ApiResponse<UserViewModel>(true, ApiResultBodyCode.Success, userViewModel);
    }

    [HttpPut("{id:int:min(1)}"), Authorize]
    public async Task<ApiResponse> UpdateUser(int id, UserUpdateViewModel userUpdateViewModel)
    {
        await _userService.UpdateAsync(id, userUpdateViewModel);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpDelete("{id:int:min(1)}"), Authorize]
    public async Task<ApiResponse> DeleteUser(int id)
    {
        await _userService.DeleteAsync(id);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    #endregion Actions
}
