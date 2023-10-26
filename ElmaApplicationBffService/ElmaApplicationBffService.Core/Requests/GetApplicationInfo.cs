using ElmaApplicationBffService.Abstractions.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElmaApplicationBffService.Core.Requests
{
    public class GetApplicationInfo : IRequest<GetApplicationInfoResponseModel>
    {
        [FromRoute(Name = "id")]
        public Guid ApplicationId { get; set; }
    }
}
