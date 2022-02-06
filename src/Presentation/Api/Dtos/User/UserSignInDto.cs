using Api.Dtos.AuthToken;
using Infrastructure.Dto.User;

namespace Api.Dtos.User;

public record class UserSignInDto
{
    public UserDto UserDto { get; init; } = null!;

    public Token Token { get; init; } = null!;
}
