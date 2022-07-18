using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDTO>().ReverseMap();
            CreateMap<PlatformCreateDTO, Platform>().ReverseMap();
            CreateMap<PlatformReadDTO, PlatformPublishedDTO>();

        }
    }
}
