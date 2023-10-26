using ElmaApplicationBffService.Abstractions.Response;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElmaApplicationBffService.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание приложения в ЭЛЬМА
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateApplication")]
        [SwaggerOperation("CreateApplication")]
        public async Task<ApiResult<CreateApplicationResponseModel>> CreateApplicationAsync(
        [FromBody] CreateApplication request)
        {
            var result = await _mediator.Send(request);
            return new ApiResult<CreateApplicationResponseModel>(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("GetApplicationInfo")]
        public async Task<ApiResult<GetApplicationInfoResponseModel>> GetApplicationInfoAsync(
        [FromBody] Guid id)
        {
            var result = await _mediator.Send(new GetApplicationInfo { ApplicationId = id });
            return new ApiResult<GetApplicationInfoResponseModel>(result);
        }
    }
}
