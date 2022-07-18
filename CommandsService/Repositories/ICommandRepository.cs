using CommandsService.Models;

namespace CommandsService.Repositories
{
    public interface ICommandRepository
    {

        //Platforms
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatformm(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);
        //Commands
        IEnumerable<Command> GetCommandsForPlatforms(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
