using ElmaApplicationBffService.Abstractions.Response;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.RestClients;

namespace ElmaApplicationBffService.Core.Requests
{
    public class RunApplicationProcessHandler : BaseRequestHandler<RunApplicationProcess, RunApplicationProcessResponseModel>
    {
        private readonly IApplicationRestClient _applicationRestClient;

        public RunApplicationProcessHandler(IApplicationRestClient applicationRestClient)
        {
            _applicationRestClient = applicationRestClient;
        }

        public override async Task<RunApplicationProcessResponseModel> HandleAsync(RunApplicationProcess request, CancellationToken cancellationToken)
        {
            var runApplicationProcessResult = await _applicationRestClient.RunApplicationProcessAsync(request.ModuleId, new RestClients.Models.RunApplicationProcess { ApplicationId = request.ApplicationId });
            
            if (runApplicationProcessResult.Result)
            {
                return new RunApplicationProcessResponseModel { ProcessId = runApplicationProcessResult.Data.ProcessId };
            }
            else
            {
                throw new ServiceException(runApplicationProcessResult.ErrorCode, runApplicationProcessResult.ErrorMessage)
                { ErrorDisplay = runApplicationProcessResult.ErrorDisplay };
            }
        }
    }
}
