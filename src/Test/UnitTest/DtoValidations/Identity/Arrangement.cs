using Api.Dtos.AuthToken;
using Infrastructure.Dto.Role;
using Infrastructure.Dto.User;

namespace UnitTest.DtoValidations.Identity;

public class Arrangement
{
    public UserCreateDto ValidUserCreateDto => new()
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

    public UserUpdateDto ValidUserUpdateDto => new()
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

    public RoleCreateUpdateDto ValidRoleCreateUpdateDto => new()
    {
        Name = "Admin",
        Description = "This is Admin Role"
    };
}
