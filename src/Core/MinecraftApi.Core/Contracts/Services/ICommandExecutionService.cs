using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;

namespace MinecraftApi.Core.Contracts.Services
{
    /// <summary>
    /// Service to execute commands.
    /// </summary>
    public interface ICommandExecutionService
    {
        /// <summary>
        /// Executes said command.
        /// </summary>
        /// <param name="commandEntity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IMinecraftResponseMessage> ExecuteAsync(ICommandEntity<SettableArgument> commandEntity, CancellationToken token);
        /// <summary>
        /// Executes a raw command.
        /// </summary>
        /// <param name="rawCommand"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IMinecraftResponseMessage> ExecuteAsync(string rawCommand, CancellationToken token);
    }
}