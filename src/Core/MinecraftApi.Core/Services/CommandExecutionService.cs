using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Extensions;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Services.Builders;

namespace MinecraftApi.Core.Services
{
    /// <summary>
    /// Executes commands
    /// </summary>
    public class CommandExecutionService : ICommandExecutionService
    {
        private readonly IRconCommandService _rconCommandService;
        private readonly ICommandService _commandService;
        private readonly IRepositoryService<BaseRanCommand> rawCommandRepositoryService;
        private readonly IRepositoryService<RanCommand> ranCommandRepositoryService;
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="rconCommandService"></param>
        /// <param name="commandService"></param>
        /// <param name="rawCommandRepositoryService"></param>
        /// <param name="ranCommandRepositoryService"></param>
        public CommandExecutionService(IRconCommandService rconCommandService,
            ICommandService commandService,
            IRepositoryService<BaseRanCommand> rawCommandRepositoryService,
            IRepositoryService<RanCommand> ranCommandRepositoryService
            )
        {
            _rconCommandService = rconCommandService;
            _commandService = commandService;
            this.rawCommandRepositoryService = rawCommandRepositoryService;
            this.ranCommandRepositoryService = ranCommandRepositoryService;
        }
        ///<inheritdoc/>
        public async Task<IMinecraftResponseMessage> ExecuteAsync(long commandId, IList<RanArgument> ranArguments, string? userId = "", CancellationToken token = default)
        {
            var savedCommand = await _commandService.RetrieveCommandAsync(commandId, token);
            if (savedCommand == null)
                throw new ArgumentException("command was not found");

            var commandBuilder = new CommandBuilder().SetPrefix(savedCommand.Prefix);



            var settableArguments = new List<SettableArgument>();
            foreach (var argument in savedCommand.Arguments!)
            {
                var valuedArgument = ranArguments.FirstOrDefault(a => a.SavedArgumentId == argument.Id);
                if (valuedArgument != null && !string.IsNullOrEmpty(valuedArgument.Value))
                {
                    commandBuilder.AddArgument(valuedArgument.Value, argument.Order);
                }
                else
                {
                    //we do have a value for this one.
                    if (argument.Required && !string.IsNullOrEmpty(argument.DefaultValue))
                        commandBuilder.AddArgument(argument.DefaultValue, argument.Order);
                    else if (argument.Required)
                        throw new ArgumentException($"Required argument is missing! id: {argument.Id} name: {argument.Name} description: {argument.Description}");
                }

            }
            var command = commandBuilder.Build();

            var commandTask = _rconCommandService.RunCommandAsync(command, token).ConfigureAwait(false);

            await LogCommandExecutionAsync(commandId, ranArguments, command, userId, token);

            return await commandTask;
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <returns></returns>
        /// 
        [Obsolete]
        public async Task<IMinecraftResponseMessage> ExecuteAsync(ICommandEntity<SettableArgument> commandEntity, string? userId = null, CancellationToken token = default)
        {
            if (commandEntity.ValidateCommand() && commandEntity.Arguments?.CheckArguments() == true)
            {
                var rawCommand = $"{commandEntity}";
                return await ExecuteAsync(rawCommand, userId, token).ConfigureAwait(false);
            }
            else if (commandEntity.Arguments?.CheckArguments() == false)
            {
                return new MinecraftResponseMessage(message: "Arguments cannot be empty if giving at least one argument. Either send arguments with values or send an empty array if no arguments are needed.", isSuccess: false);
            }
            else
            {
                //we need to load the command.
                var savedCommand = await _commandService.RetrieveCommandAsync(commandEntity.Id, token);
                if (savedCommand != null)
                {
                    commandEntity.CopyFrom(savedCommand);
                    if (commandEntity.Arguments?.Any() == true)
                    {
                        if (commandEntity.Arguments?.CheckArguments() == true)
                        {
                            var rawCommand = $"{commandEntity}";
                            return await ExecuteAsync(rawCommand, userId, token);
                        }
                        throw new InvalidOperationException("Arguments cannot be null if running a command, send an empty list if no arguments are given.");
                    }
                    else
                    {
                        var rawCommand = $"{commandEntity}";
                        return await ExecuteAsync(rawCommand, userId, token);
                    }
                }
                throw new InvalidOperationException("Command not found.");
            }
        }
        /// <inheritdoc/>
        public async Task<IMinecraftResponseMessage> ExecuteAsync(string rawCommand, string? userId = null, CancellationToken token = default)
        {
            var commandTask = _rconCommandService.RunCommandAsync(rawCommand, token);
            var logTask = LogCommandExecutionAsync(rawCommand, userId, token: token);

            await Task.WhenAll(commandTask, logTask); //you don't want things to start getting disposed of before the logging is done. But you don't want to slow down the command.
            return await commandTask;
        }
        /// <summary>
        /// This will log the execution of a command but it is meant to be safely ran as a fire and forget task since it is try catched for every possible exception.
        /// </summary>
        /// <param name="rawCommand"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task LogCommandExecutionAsync(string rawCommand, string? userId = null, CancellationToken token = default)
        {
            try
            {
                var baseSavedCommand = new BaseRanCommand { RanTime = DateTime.UtcNow, RawCommand = rawCommand, UserId = userId };
                await rawCommandRepositoryService.CreateAsync(baseSavedCommand);
            }
            catch { }
        }

        private async Task LogCommandExecutionAsync(long commandId, IList<RanArgument> arguments, string translatedCommand, string? userId = null, CancellationToken token = default)
        {
            try
            {
                var savedCommand = new RanCommand { CommandId = commandId, RanTime = DateTime.UtcNow, UserId = userId, RawCommand = translatedCommand, RanArguments = arguments };
                await ranCommandRepositoryService.CreateAsync(savedCommand, token);
            }
            catch { }
        }
    }
}
