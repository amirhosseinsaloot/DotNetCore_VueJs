using Core.Enums;
using System.Net;

namespace Core.Exceptions;

/// <summary>
/// Represents errors that occur when a received Security Token has expiration time in the past.
/// </summary>
public sealed class TokenExpiredException : BaseWebException
{
    public TokenExpiredException(string message = "Authenticate failure.", object additionalData = null)
      : base(message, HttpStatusCode.Unauthorized, ApiResultBodyCode.ExpiredSecurityToken, additionalData)
    {
    }
}