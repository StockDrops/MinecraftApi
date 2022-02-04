using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Ef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Services
{
    /// <summary>
    /// Handles all the CRUD operations around plugins.
    /// </summary>
    public class ArgumentService
    {
        private readonly PluginContext _pluginContext;
        /// <summary>
        /// Create a plugin service with its db context.
        /// </summary>
        /// <param name="pluginContext"></param>
        public ArgumentService(PluginContext pluginContext)
        {
            _pluginContext = pluginContext;
        }
        /// <summary>
        /// Saves a new command for an existing plugin.
        /// </summary>
        /// <param name="argument">The aregument to save</param>
        /// <param name="commandId">The id of the command to apply it to</param>
        /// <returns></returns>
        public Task SaveAsync(IArgumentEntity argument, long commandId)
        {
            var c = new SavedArgument(argument);
            c.CommandId = commandId;
            _pluginContext.Arguments?.Add(c);
            return _pluginContext.SaveChangesAsync();
        }
        /// <summary>
        /// Retrieve an argument saved in the database by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Null if there's no command by that id.</returns>
        public async Task<SavedArgument?> RetrieveCommandAsync(long id, CancellationToken cancellationToken)
        {
            if (_pluginContext.Arguments == null)
                return null;
            else
                return await _pluginContext.Arguments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        /// <summary>
        /// Returns all arguments containing that search string in their prefix.
        /// List is empty if no plugin found.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Command>> SearchCommandByPrefix(string search, CancellationToken cancellationToken)
        {
            var results = new List<Command>();
            if(_pluginContext.Commands == null)
            {
                return results;
            }
            return await _pluginContext.Commands.Where(p => p.Prefix != null && p.Prefix.Contains(search)).ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Returns all plugins containing that search string in their name.
        /// List is empty if no plugin found.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Command>> SearchCommandByName(string search, CancellationToken cancellationToken)
        {
            var results = new List<Command>();
            if (_pluginContext.Commands == null)
            {
                return results;
            }
            return await _pluginContext.Commands.Where(p => p.Name != null && p.Name.Contains(search)).ToListAsync(cancellationToken);
        }
    }
}
