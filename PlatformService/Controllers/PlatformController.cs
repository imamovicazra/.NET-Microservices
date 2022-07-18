using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.Repositories;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(IPlatformRepository platformRepository,
           IMapper mapper,
           ICommandDataClient commandDataClient,
           IMessageBusClient messageBusClient)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
            _commandDataClient= commandDataClient;
            _messageBusClient= messageBusClient;    
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            var items = _platformRepository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(items));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id)
        {
            var item = _platformRepository.GetPlatformById(id);
            if (item == null)
            {
                return NotFound();
            }
            else
                return Ok(_mapper.Map<PlatformReadDTO>(item));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDTO);
            _platformRepository.CreatePlatform(platformModel);
            var platformReadDto = _mapper.Map<PlatformReadDTO>(platformModel);

            //Send Sync Message
            try
            {
                await _commandDataClient.SendPlatfromToCommand(platformReadDto);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDTO>(platformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}
