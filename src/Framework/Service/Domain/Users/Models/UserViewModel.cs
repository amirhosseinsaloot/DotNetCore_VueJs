using Core.Enums;
using Core.Interfaces;

namespace Services.Domain;

public class UserViewModel : IViewModel
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public GenderType Gender { get; set; }

    public ICollection<string> Roles { get; set; }

    public int TeamId { get; set; }

    public int? ProfilePictureId { get; set; }

    public bool IsActive { get; set; }
}
