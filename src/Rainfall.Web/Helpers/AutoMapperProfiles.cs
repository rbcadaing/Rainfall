using AutoMapper;
using Rainfall.Core.Dto;
using Rainfall.Core.Model;

namespace Rainfall.Web.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Item, EnvironmentDataStationResponseDto>();

            CreateMap<EnvironmentDataStationResponseDto,Item>();
        }
    }
}
