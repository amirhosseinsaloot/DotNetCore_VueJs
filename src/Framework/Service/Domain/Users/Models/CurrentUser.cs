namespace Service.Domain.Users.Models;

public record class CurrentUser
{
    public int Id { get; init; }

    public string Username { get; init; } = default!;

    public string Firstname { get; init; } = default!;

    public string Lastname { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime Birthdate { get; init; }

    public string PhoneNumber { get; init; } = default!;

    public GenderType Gender { get; init; }

    public ICollection<string> Roles { get; init; } = default!;

    public int TeamId { get; init; }

    public int TenantId { get; init; }
}
