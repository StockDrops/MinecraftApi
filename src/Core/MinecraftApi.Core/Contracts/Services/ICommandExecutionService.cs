using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Rcon.Contracts.Models;

namespace MinecraftApi.Core.Contracts.Services
{
    /// <summary>
    /// Service to execute commands.
    /// </summary>
    public interface ICommandExecutionService
    {
        /// <summary>
        /// Runs a command using its id, and a list of arguments that can be saved.
        /// </summary>
        /// <param name="commandId"></param>
        /// <param name="ranArguments"></param>
        /// <param name="userId">user id used for loging.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<IMinecraftResponseMessage> ExecuteAsync(long commandId, IList<RanArgument> ranArguments, string? userId = null, CancellationToken token = default);
        /// <summary>
        /// Executes said command.
        /// </summary>
        /// <param name="commandEntity"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IMinecraftResponseMessage> ExecuteAsync(ICommandEntity<SettableArgument> commandEntity, string? userId = null, CancellationToken token = default);
        /// <summary>
        /// Executes a raw command.
        /// </summary>
        /// <param name="rawCommand"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IMinecraftResponseMessage> ExecuteAsync(string rawCommand, string? userId = null, CancellationToken token = default);
    }
}