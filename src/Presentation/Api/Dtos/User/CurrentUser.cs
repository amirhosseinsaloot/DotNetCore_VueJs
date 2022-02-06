namespace Api.Dtos.User;

public record class CurrentUser
{
    public int Id { get; init; }

    public string Username { get; init; } = null!;

    public string Firstname { get; init; } = null!;

    public string Lastname { get; init; } = null!;

    public string Email { get; init; } = null!;

    public DateTime Birthdate { get; init; }

    public string PhoneNumber { get; init; } = null!;

    public GenderType Gender { get; init; }

    public ICollection<string> Roles { get; init; } = null!;

    public int TeamId { get; init; }

    public int TenantId { get; init; }
}
