using Service.Domain.Roles.Models;
using Service.Domain.Users.Models;
using Service.Identity.Models;

namespace UnitTest.ViewModelValidations.Identity;

public class Arrangement
{
    public UserCreateViewModel ValidUserCreateViewModel => new()
    {
        Username = "Admin",
        Firstname = "Brad",
        Lastname = "Pitt",
        Email = "BradPitt@gmail.com",
        Password = "123456",
        Birthdate = DateTime.UtcNow,
        PhoneNumber = "(281)388-0388",
        Gender = GenderType.Male,
        TeamId = 1
    };

    public UserUpdateViewModel ValidUserUpdateViewModel => new()
    {
        Username = "Admin",
        Firstname = "Brad",
        Lastname = "Pitt",
        Email = "BradPitt@gmail.com",
        Birthdate = DateTime.UtcNow,
        PhoneNumber = "(281)388-0388",
        Gender = GenderType.Male,
        TeamId = 1,
    };

    public TokenRequest ValidTokenRequest => new()
    {
        GrantType = "password",
        Username = "Admin",
        Password = "123456",
        AccessToken = "",
        RefreshToken = ""
    };

    public RoleCreateUpdateViewModel ValidRoleCreateUpdateViewModel => new()
    {
        Name = "Admin",
        Description = "This is Admin Role"
    };
}
