using ElmaApplicationBffService.Abstractions.Response;

namespace ElmaApplicationBffService.Core.Infrastructure
{
    public class ValidationExceptionApiResult : ApiResult
    {
        public IDictionary<string, string[]> ModelState { get; set; }

        public ValidationExceptionApiResult(IDictionary<string, string[]> modelState, string errorCode, string errorMessage, string errorDisplay = null, string debugMessage = null)
            : base(errorCode, errorMessage, errorDisplay, debugMessage)
        {
            ModelState = modelState;
        }
    }
}
