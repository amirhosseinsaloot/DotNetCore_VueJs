using Infrastructure.Dto.User;
using Infrastructure.Services.Jwt.Models;

namespace Infrastructure.Services.Identity.Models;

public record class UserSignInDto
{
    public UserDto UserDto { get; init; } = null!;

    public Token Token { get; init; } = null!;
}
