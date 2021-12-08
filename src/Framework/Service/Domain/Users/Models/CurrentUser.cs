namespace Service.Domain.Users.Models;

public record class CurrentUser
{
    public int Id { get; init; }

    public string Username { get; init; }

    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public string Email { get; init; }

    public DateTime Birthdate { get; init; }

    public string PhoneNumber { get; init; }

    public GenderType Gender { get; init; }

    public ICollection<string> Roles { get; init; }

    public int TeamId { get; init; }

    public int TenantId { get; init; }
}
