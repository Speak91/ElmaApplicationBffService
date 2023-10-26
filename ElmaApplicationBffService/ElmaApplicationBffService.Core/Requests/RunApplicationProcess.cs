using System;
using ElmaApplicationBffService.Abstractions.Response;
using MediatR;

namespace ElmaApplicationBffService.Core.Requests
{
    public class RunApplicationProcess : IRequest<RunApplicationProcessResponseModel>
    {
        public Guid ApplicationId { get; set; }
        public Guid ModuleId { get; set; }
    }
}
