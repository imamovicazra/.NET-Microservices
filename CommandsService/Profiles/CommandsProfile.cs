using AutoMapper;
using CommandsService.DTOs;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDTO>().ReverseMap();
            CreateMap<CommandCreateDTO, Command>().ReverseMap();
            CreateMap<Command, CommandReadDTO>().ReverseMap();
        }

    }
}
