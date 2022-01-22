using Service.Domain.Users.Models;
using Service.Identity.Models;
using Service.Jwt.Models;

namespace Service.Identity.Services;

public interface IAuthService
{
    Task<UserSignInViewModel> SignInAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);

    Task<UserSignInViewModel> RegisterAsync(UserCreateViewModel userDto, CancellationToken cancellationToken);

    Task<Token> RefreshTokenAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);
}
