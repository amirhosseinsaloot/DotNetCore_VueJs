using Core.Enums;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Domain;
using Services.Jwt;

namespace Api.Controllers;

public class AuthorizationController : BaseController
{
    #region Fields

    private readonly IAuthService _authService;

    #endregion Fields

    #region Ctor

    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    #endregion Ctor

    #region Action

    [HttpPost("[action]"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInViewModel>> Login(TokenRequest tokenRequest)
    {
        var token = await _authService.SignInAsync(tokenRequest);
        return new ApiResponse<UserSignInViewModel>(true, ApiResultBodyCode.Success, token);
    }

    [HttpPost("[action]"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInViewModel>> Register(UserCreateViewModel userDto)
    {
        return new ApiResponse<UserSignInViewModel>(true, ApiResultBodyCode.Success, await _authService.RegisterAsync(userDto));
    }

    [HttpPost("[action]"), Authorize]
    public async Task<ApiResponse> Logout()
    {
        await _authService.LogoutAsync(User);
        return new ApiResponse(true, ApiResultBodyCode.Success);
    }

    [HttpPost("[action]"), AllowAnonymous]
    public async Task<ApiResponse<Token>> RefreshToken(TokenRequest tokenRequest)
    {
        var token = await _authService.RefreshTokenAsync(tokenRequest);
        return new ApiResponse<Token>(true, ApiResultBodyCode.Success, token);
    }

    #endregion Action
}
