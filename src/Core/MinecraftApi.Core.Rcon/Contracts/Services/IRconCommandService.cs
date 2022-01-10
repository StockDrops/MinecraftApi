using MinecraftApi.Core.Rcon.Contracts.Models;

namespace MinecraftApi.Core.Rcon.Contracts.Services
{
    /// <summary>
    /// Runs a command
    /// </summary>
    public interface IRconCommandService
    {
        /// <summary>
        /// Runs a command asynchronously
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IMinecraftResponseMessage> RunCommandAsync(string command, CancellationToken cancellationToken);
    }
}