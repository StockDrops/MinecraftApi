using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;

namespace MinecraftApi.Api.Services
{
    /// <summary>
    /// Executes commands
    /// </summary>
    public class CommandExecutionService : ICommandExecutionService
    {
        private readonly IRconCommandService _rconCommandService;
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="rconCommandService"></param>
        public CommandExecutionService(IRconCommandService rconCommandService)
        {
            _rconCommandService = rconCommandService;
        }
        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <returns></returns>
        public async Task<IMinecraftResponseMessage> ExecuteAsync(ICommandEntity<SettableArgument> commandEntity, CancellationToken token)
        {
            return await _rconCommandService.RunCommandAsync($"{commandEntity}", token);
        }
        /// <inheritdoc/>
        public Task<IMinecraftResponseMessage> ExecuteAsync(string rawCommand, CancellationToken token)
        {
            return _rconCommandService.RunCommandAsync(rawCommand, token);
        }
    }
}
