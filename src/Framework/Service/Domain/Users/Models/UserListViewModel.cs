namespace Service.Domain.Users.Models;

public record class UserListViewModel : IListViewModel
{
    public int Id { get; init; }

    public string Username { get; init; } = default!;

    public string Firstname { get; init; } = default!;

    public string Lastname { get; init; } = default!;

    public string Email { get; init; } = default!;

    public DateTime Birthdate { get; init; }

    public string? PhoneNumber { get; init; }

    public GenderType Gender { get; init; }

    public int TeamId { get; init; }

    public bool IsActive { get; init; }
}
