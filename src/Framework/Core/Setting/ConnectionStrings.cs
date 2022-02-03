using Microsoft.Extensions.Options;

namespace Core.Setting;

public sealed record class ConnectionStrings
{
    public string? SqlServer { get; init; }

    public string? Postgres { get; init; }
}

public class ConnectionStringsValidation : IValidateOptions<ConnectionStrings>
{
    public ValidateOptionsResult Validate(string name, ConnectionStrings options)
    {
        if (string.IsNullOrEmpty(options.SqlServer)
            && string.IsNullOrEmpty(options.Postgres))
        {
            return ValidateOptionsResult.Fail($"{nameof(ConnectionStrings)} can not be empty (Choose between {nameof(DatabaseProvider.Postgres)} and {nameof(DatabaseProvider.SqlServer)})");
        }

        return ValidateOptionsResult.Success;
    }
}