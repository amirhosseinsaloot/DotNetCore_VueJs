using Infrastructure.Dto.User;
using Infrastructure.Services.Identity.Models;
using Infrastructure.Services.Identity.Services;
using Infrastructure.Services.Jwt.Models;

namespace Api.Controllers;

public class AuthorizationController : BaseController
{
    private readonly IAuthService _authService;

    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInDto>> Login(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var token = await _authService.SignInAsync(tokenRequest, cancellationToken);
        return new ApiResponse<UserSignInDto>(true, ApiResultBodyCode.Success, token);
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<ApiResponse<UserSignInDto>> Register(UserCreateDto userDto, CancellationToken cancellationToken)
    {
        return new ApiResponse<UserSignInDto>(true, ApiResultBodyCode.Success, await _authService.RegisterAsync(userDto, cancellationToken));
    }

    [HttpPost("RefreshToken"), AllowAnonymous]
    public async Task<ApiResponse<Token>> RefreshToken(TokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var token = await _authService.RefreshTokenAsync(tokenRequest, cancellationToken);
        return new ApiResponse<Token>(true, ApiResultBodyCode.Success, token);
    }
}