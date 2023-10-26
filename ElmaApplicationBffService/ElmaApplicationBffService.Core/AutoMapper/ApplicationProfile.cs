using AutoMapper;
using ElmaApplicationBffService.Abstractions.Response;
using ElmaApplicationBffService.Core.Requests;

namespace ElmaApplicationBffService.Core.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateApplication, ElmaApplicationBffService.Core.RestClients.Models.CreateApplication>();
            CreateMap<ElmaApplicationBffService.Core.RestClients.Models.GetApplicationInfoResponseModel, GetApplicationInfoResponseModel>();
        }
    }
}
