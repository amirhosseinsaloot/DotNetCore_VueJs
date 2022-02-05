using Infrastructure.Dto.User;
using Infrastructure.Services.Identity.Models;
using Infrastructure.Services.Jwt.Models;

namespace Infrastructure.Services.Identity.Services;

public interface IAuthService
{
    Task<UserSignInDto> SignInAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);

    Task<UserSignInDto> RegisterAsync(UserCreateDto userDto, CancellationToken cancellationToken);

    Task<Token> RefreshTokenAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);
}
