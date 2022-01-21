namespace Core.Exceptions;

/// <summary>
/// Represents errors when the requested data could not be found.
/// </summary>
public sealed class NotFoundException : BaseWebApiException
{
    public NotFoundException(string message = "Not found", object? additionalData = null)
       : base(message, HttpStatusCode.NotFound, ApiResultBodyCode.NotFound, additionalData)
    {
    }
}
