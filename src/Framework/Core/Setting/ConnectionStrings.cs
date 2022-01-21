namespace Core.Setting;

public sealed record class ConnectionStrings
{
    public string? SqlServer { get; init; } 

    public string? Postgres { get; init; }
}
