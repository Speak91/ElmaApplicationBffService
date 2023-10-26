using AutoMapper;
using ElmaApplicationBffService.Abstractions.Response;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.Options;
using ElmaApplicationBffService.Core.RestClients;
using MediatR;
using Microsoft.Extensions.Options;

namespace ElmaApplicationBffService.Core.Requests
{
    public class CreateApplicationHandler : BaseRequestHandler<CreateApplication, CreateApplicationResponseModel>
    {
        private readonly IApplicationRestClient _applicationRestClient;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string _elmaModuleId;

        public CreateApplicationHandler(IApplicationRestClient applicationRestClient,
            IMapper mapper, IOptions<ElmaOptions> options, IMediator mediator)
        {
            _applicationRestClient = applicationRestClient;
            _mediator = mediator;
            _mapper = mapper;
            _elmaModuleId = options.Value.ModuleId;
        }

        public async override Task<CreateApplicationResponseModel> HandleAsync(CreateApplication request, CancellationToken cancellationToken)
        {
            var elmaRequest = _mapper.Map<RestClients.Models.CreateApplication>(request);
            var application = await _applicationRestClient.CreateApplicationAsync(new Guid(_elmaModuleId), elmaRequest);
            if (application.Result)
            {
                await _mediator.Send(new RunApplicationProcess { ApplicationId = application.Data.ApplicationId, ModuleId = new Guid(_elmaModuleId) });
                return new CreateApplicationResponseModel
                {
                    ApplicationId = application.Data.ApplicationId
                };

            }
            else
            {
                throw new ServiceException(application.ErrorCode, application.ErrorMessage)
                { ErrorDisplay = application.ErrorDisplay };
            }
        }
    }
}
