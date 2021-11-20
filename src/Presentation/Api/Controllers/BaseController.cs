using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Service.Domain.Users.Models;

namespace Api.Controllers;

[Route("api/[controller]"), ApiController]
public class BaseController : ControllerBase
{
    public CurrentUser CurrentUser => new CurrentUser
    {
        Id = int.Parse(User.FindFirst("UserId").Value),
        Username = User.FindFirst("Username").Value,
        Firstname = User.FindFirst("Firstname").Value,
        Lastname = User.FindFirst("Lastname").Value,
        Email = User.FindFirst("Email").Value,
        Birthdate = DateTime.Parse(User.FindFirst("Birthdate").Value),
        PhoneNumber = User.FindFirst("PhoneNumber").Value,
        Gender = Enum.Parse<GenderType>(User.FindFirst("Gender").Value),
        Roles = User.FindFirst("Roles").Value.Split(',').ToList(),
        TeamId = int.Parse(User.FindFirst("TeamId").Value),
        TenantId = int.Parse(User.FindFirst("TenantId").Value),
    };
}
