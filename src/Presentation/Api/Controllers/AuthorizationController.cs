using Service.DomainDto.User;
using Service.Identity.Models;
using Service.Identity.Services;
using Service.Jwt.Models;

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

    #endregion Action
}
