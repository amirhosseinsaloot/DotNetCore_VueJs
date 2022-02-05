using Service.DomainDto.User;
using Service.Identity.Models;
using Service.Jwt.Models;

namespace Service.Identity.Services;

public interface IAuthService
{
    Task<UserSignInDto> SignInAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);

    Task<UserSignInDto> RegisterAsync(UserCreateDto userDto, CancellationToken cancellationToken);

    Task<Token> RefreshTokenAsync(TokenRequest tokenRequest, CancellationToken cancellationToken);
}
