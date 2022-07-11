namespace Domain.Exceptions;

/// <summary>
/// Represents errors that occur when server understands the request but refuses to authorize it (unauthorized to access intended resource).
/// </summary>
public sealed class ForbiddenException : BaseWebApiException
{
    public ForbiddenException(string message = "You are unauthorized to access this resource.", object? additionalData = null)
      : base(message, HttpStatusCode.Forbidden, ApiResultBodyCode.Forbidden, additionalData)
    {
    }
}