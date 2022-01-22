using Service.Domain.Users.Models;
using Service.Jwt.Models;

namespace Service.Identity.Models;

public record class UserSignInViewModel
{
    public UserViewModel UserViewModel { get; init; } = default!;

    public Token Token { get; init; } = default!;
}
