using Core.Enums;
using System.Net;

namespace Core.Exceptions;

/// <summary>
/// Represents errors that occur when invalid arguments passed.
/// </summary>
public sealed class BadRequestException : BaseWebApiException
{
    public BadRequestException(string message = "Bad Request", object additionalData = null)
       : base(message, HttpStatusCode.BadRequest, ApiResultBodyCode.BadRequest, additionalData)
    {
    }
}
