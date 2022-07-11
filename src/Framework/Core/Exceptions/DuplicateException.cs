namespace Domain.Exceptions;

/// <summary>
/// Represents errors that occur when an attempt was made to save a duplicate record .
/// </summary>
public sealed class DuplicateException : BaseWebApiException
{
    public DuplicateException(string message = "Duplication", object? additionalData = null)
       : base(message, HttpStatusCode.Conflict, ApiResultBodyCode.Duplication, additionalData)
    {
    }
}