using AutoMapper;
using ElmaApplicationBffService.Abstractions.Response;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.Options;
using ElmaApplicationBffService.Core.RestClients;
using Microsoft.Extensions.Options;

namespace ElmaApplicationBffService.Core.Requests
{
    public class GetApplicationInfoHandler : BaseRequestHandler<GetApplicationInfo, GetApplicationInfoResponseModel>
    {
        private readonly IApplicationRestClient _applicationRestClient;
        private readonly string _elmaModuleId;
        private readonly IMapper _mapper;

        public GetApplicationInfoHandler(IApplicationRestClient applicationRestClient, 
            IOptions<ElmaOptions> options, IMapper mapper)
        {
            _applicationRestClient = applicationRestClient;
            _elmaModuleId = options.Value.ModuleId;
            _mapper = mapper;
        }

        public override async Task<GetApplicationInfoResponseModel> HandleAsync(GetApplicationInfo request, CancellationToken cancellationToken)
        {
            var elmaResponse = await _applicationRestClient.GetApplicationInfoAsync(new Guid(_elmaModuleId), new ElmaApplicationBffService.Core.RestClients.Models.GetApplicationInfo  { ApplicationId = request.ApplicationId});
            return _mapper.Map<GetApplicationInfoResponseModel>(elmaResponse.Data);
        }
    }
}
