namespace Core.Exceptions;

/// <summary>
/// Represents errors that occur when authenticate failure or need to authentication .
/// </summary>
public sealed class AuthenticationException : BaseWebApiException
{
    public AuthenticationException(string message = "Authenticate failure.", object? additionalData = null)
      : base(message, HttpStatusCode.Unauthorized, ApiResultBodyCode.UnAuthorized, additionalData)
    {
    }
}