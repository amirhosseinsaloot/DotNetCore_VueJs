using Services.Jwt;

namespace Services.Domain;

public record class UserSignInViewModel
{
    public UserViewModel UserViewModel { get; init; }

    public Token Token { get; init; }
}
