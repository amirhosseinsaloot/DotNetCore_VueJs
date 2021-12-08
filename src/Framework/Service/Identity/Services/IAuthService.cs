using Service.Domain.Users.Models;
using Service.Identity.Models;
using Service.Jwt.Models;
using System.Security.Claims;

namespace Service.Identity.Services;

public interface IAuthService
{
    Task<UserSignInViewModel> SignInAsync(TokenRequest tokenRequest);

    Task<UserSignInViewModel> RegisterAsync(UserCreateViewModel userDto);

    Task LogoutAsync(ClaimsPrincipal claimsPrincipal);

    Task<Token> RefreshTokenAsync(TokenRequest tokenRequest);
}
