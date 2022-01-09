using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Plugin base class.
    /// </summary>
    /// 
    public class Plugin : IPlugin<Command, Argument> 
    {
        /// <inheritdoc/>
        public long Id { get; set; }
        /// <inheritdoc/>
        public IList<Command>? Commands { get; set; }
        
        /// <inheritdoc/>
        public string? Name { get; set; }

        /// <summary>
        /// Create an empty plugin.
        /// </summary>
        public Plugin() { }
        /// <summary>
        /// Creates a plugin from an IPlugin. Id is set to 0.
        /// </summary>
        /// <param name="plugin"></param>
        public Plugin(IPlugin<Command, Argument> plugin)
        {
            Name = plugin.Name;
            Id = 0;
            Commands = plugin.Commands?.Select(x => new Command(x)).ToList();
        }
    }
}
