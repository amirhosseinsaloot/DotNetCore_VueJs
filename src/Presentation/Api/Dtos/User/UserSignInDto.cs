using Api.Dtos.AuthToken;

namespace Api.Dtos.User;

public record class UserSignInDto
{
    public UserDto UserDto { get; init; } = null!;

    public Token Token { get; init; } = null!;
}
