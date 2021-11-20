using Core.Enums;
using System.Net;

namespace Core.Exceptions;

/// <summary>
/// Represents errors when the requested data could not be found.
/// </summary>
public sealed class NotFoundException : BaseWebException
{
    public NotFoundException(string message = "Not found", object additionalData = null)
       : base(message, HttpStatusCode.NotFound, ApiResultBodyCode.NotFound, additionalData)
    {
    }
}
