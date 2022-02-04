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
    public class PluginService
    {
        private readonly PluginContext _pluginContext;
        /// <summary>
        /// Create a plugin service with its db context.
        /// </summary>
        /// <param name="pluginContext"></param>
        public PluginService(PluginContext pluginContext)
        {
            _pluginContext = pluginContext;
        }
        /// <summary>
        /// Saves a new plugin.
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public Task SaveAsync(IPlugin<Command, SavedArgument> plugin)
        {
           _pluginContext.Plugins?.Add(new Plugin(plugin)) ;
            return _pluginContext.SaveChangesAsync();
        } 
        /// <summary>
        /// Retrieve a plugin saved in the database by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Plugin?> RetrievePluginAsync(long id)
        {
            if (_pluginContext.Plugins == null)
                return null;
            else
                return await _pluginContext.Plugins.Include(p => p.Commands).ThenInclude(c => c.Arguments).FirstOrDefaultAsync(x => x.Id == id);
        }
        /// <summary>
        /// Returns all plugins containing that search string in their name.
        /// List is empty if no plugin found.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IList<Plugin>> SearchPluginByName(string search)
        {
            var results = new List<Plugin>();
            if(_pluginContext.Plugins == null)
            {
                return results;
            }
            return await _pluginContext.Plugins.Where(p => p.Name != null && p.Name.Contains(search)).ToListAsync();
        }
    }
}
