using Service.Domain.Users.Models;

namespace Api.Controllers;

[Route("api/[controller]"), ApiController]
public class BaseController : ControllerBase
{
    public CurrentUser CurrentUser => new CurrentUser
    {
        Id = int.Parse(User.FindFirst(nameof(CurrentUser.Id)).Value),
        Username = User.FindFirst(nameof(CurrentUser.Username)).Value,
        Firstname = User.FindFirst(nameof(CurrentUser.Firstname)).Value,
        Lastname = User.FindFirst(nameof(CurrentUser.Lastname)).Value,
        Email = User.FindFirst(nameof(CurrentUser.Email)).Value,
        Birthdate = DateTime.Parse(User.FindFirst(nameof(CurrentUser.Birthdate)).Value),
        PhoneNumber = User.FindFirst(nameof(CurrentUser.PhoneNumber)).Value,
        Gender = Enum.Parse<GenderType>(User.FindFirst(nameof(CurrentUser.Gender)).Value),
        Roles = User.FindFirst(nameof(CurrentUser.Roles)).Value.Split(',').ToList(),
        TeamId = int.Parse(User.FindFirst(nameof(CurrentUser.TeamId)).Value),
        TenantId = int.Parse(User.FindFirst(nameof(CurrentUser.TenantId)).Value),
    };
}
