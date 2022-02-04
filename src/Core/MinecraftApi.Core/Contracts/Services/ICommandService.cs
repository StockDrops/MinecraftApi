using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;

namespace MinecraftApi.Core.Contracts.Services;
/// <summary>
/// Command Service definition. In charge of all CRUD operations around commands.
/// </summary>
public interface ICommandService
{
    /// <summary>
    /// Retrieve a plugin saved in the database by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Null if there's no command by that id.</returns>
    Task<Command?> RetrieveCommandAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Saves the command linking it to a plugin.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="pluginId"></param>
    /// <returns></returns>
    Task SaveAsync(ICommandEntity<SavedArgument> command, long pluginId);
    /// <summary>
    /// Search a command by a name.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IList<Command>> SearchCommandByName(string search, CancellationToken cancellationToken);
    /// <summary>
    /// Search a command by a prefix.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IList<Command>> SearchCommandByPrefix(string search, CancellationToken cancellationToken);
}
