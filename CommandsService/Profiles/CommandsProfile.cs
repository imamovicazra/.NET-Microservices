using AutoMapper;
using CommandsService.DTOs;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDTO>().ReverseMap();
            CreateMap<CommandCreateDTO, Command>().ReverseMap();
            CreateMap<Command, CommandReadDTO>().ReverseMap();
            CreateMap<PlatformPublishedDTO, Platform>()
               .ForMember(dest => dest.ExternalID,
               opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
               .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }

    }
}
