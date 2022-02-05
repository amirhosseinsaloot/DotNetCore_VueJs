using Service.DomainDto.User;
using Service.Jwt.Models;

namespace Service.Identity.Models;

public record class UserSignInDto
{
    public UserDto UserDto { get; init; } = null!;

    public Token Token { get; init; } = null!;
}
