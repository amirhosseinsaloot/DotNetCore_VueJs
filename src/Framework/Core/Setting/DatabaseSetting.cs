namespace Core.Setting;

public sealed record class DatabaseSetting
{
    public ConnectionStrings? ConnectionStrings { get; init; }

    public bool StoreFilesOnDatabase { get; init; } = true;

    public DatabaseProvider DatabaseProvider { get; init; } = DatabaseProvider.Postgres;
}
