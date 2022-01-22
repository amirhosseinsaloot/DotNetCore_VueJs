namespace Service.Domain.Users.Models;

public class UserViewModel : IViewModel
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;

    public string Firstname { get; set; } = default!;

    public string Lastname { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime Birthdate { get; set; }

    public string? PhoneNumber { get; set; }

    public GenderType Gender { get; set; }

    public ICollection<string> Roles { get; set; } = default!;

    public int TeamId { get; set; }

    public int? ProfilePictureId { get; set; }

    public bool IsActive { get; set; }
}
