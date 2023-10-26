using Refit;
using Microsoft.AspNetCore.Mvc;
using ElmaApplicationBffService.Core.RestClients.Models;

namespace ElmaApplicationBffService.Core.RestClients
{
    [Headers("Content-Type: application/json", "Authorization: Bearer")]
    public interface IApplicationRestClient
    {
        [Post("/api/extensions/{moduleId}/script/CreateApplication")]
        Task<Abstractions.Response.ApiResult<CreateApplicationResponseModel>> CreateApplicationAsync(Guid moduleId, [Body] CreateApplication request);

        [Post("/api/extensions/{moduleId}/script/RunApplicationProcess")]
        Task<Abstractions.Response.ApiResult<Models.RunApplicationProcessResponseModel>> RunApplicationProcessAsync(Guid moduleId, [Body] RunApplicationProcess request);

        [Get("/api/extensions/{moduleId}/script/GetApplicationInfo")]
        Task<Abstractions.Response.ApiResult<Models.GetApplicationInfoResponseModel>> GetApplicationInfoAsync(Guid moduleId, [FromQuery] GetApplicationInfo request);
    }
}
