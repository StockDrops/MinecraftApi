using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Extensions;
using MinecraftApi.Rcon.Models;

namespace MinecraftApi.Api.Services
{
    /// <summary>
    /// Executes commands
    /// </summary>
    public class CommandExecutionService : ICommandExecutionService
    {
        private readonly IRconCommandService _rconCommandService;
        private readonly ICommandService _commandService;
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="rconCommandService"></param>
        /// <param name="commandService"></param>
        public CommandExecutionService(IRconCommandService rconCommandService,
            ICommandService commandService
            )
        {
            _rconCommandService = rconCommandService;
            _commandService = commandService;
        }
        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <returns></returns>
        public async Task<IMinecraftResponseMessage> ExecuteAsync(ICommandEntity<SettableArgument> commandEntity, CancellationToken token)
        {
            if (commandEntity.ValidateCommand() && commandEntity.Arguments?.CheckArguments() == true)
            {
                return await _rconCommandService.RunCommandAsync($"{commandEntity}", token);
            }
            else if(commandEntity.Arguments?.CheckArguments() == false)
            {
                return new MinecraftResponseMessage(message: "Arguments cannot be empty if giving at least one argument. Either send arguments with values or send an empty array if no arguments are needed.", isSuccess: false);
            }
            else
            {
                //we need to load the command.
                var savedCommand = await _commandService.RetrieveCommandAsync(commandEntity.Id, token);
                if (savedCommand != null) {
                    commandEntity.CopyFrom(savedCommand);
                    if (commandEntity.Arguments?.Any() == true)
                    {
                        if (commandEntity.Arguments?.CheckArguments() == true)
                        {
                            return await _rconCommandService.RunCommandAsync($"{commandEntity}", token);
                        }
                        throw new InvalidOperationException("Arguments cannot be null if running a command, send an empty list if no arguments are given.");
                    }
                    else
                    {
                        return await _rconCommandService.RunCommandAsync($"{commandEntity}", token);
                    }
                }
                throw new InvalidOperationException("Command not found.");
            }
        }
        /// <inheritdoc/>
        public Task<IMinecraftResponseMessage> ExecuteAsync(string rawCommand, CancellationToken token)
        {
            return _rconCommandService.RunCommandAsync(rawCommand, token);
        }
    }
}
