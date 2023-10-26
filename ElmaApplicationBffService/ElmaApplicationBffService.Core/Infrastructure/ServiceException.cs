using System.Collections;

namespace ElmaApplicationBffService.Core.Infrastructure;

public class ServiceException : Exception
{
    public const string UnknownErrorCode = "UNKNOWN";

    public string ErrorCode { get; }

    public ICollection Errors { get; }

    public string ErrorDisplay { get; set; }

    public ServiceException(string errorCode, Exception innerException = null, ICollection errors = null)
        : base("See message by errorCode = '" + errorCode + "'", innerException)
    {
        ErrorCode = errorCode;
        Errors = errors;
    }

    public ServiceException(string errorCode, string message, Exception innerException = null, ICollection errors = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Errors = errors;
    }
}
