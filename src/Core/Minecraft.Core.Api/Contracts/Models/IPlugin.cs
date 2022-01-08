using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// Defines a plugin used on Minecraft.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Commands defined for the plugin.
        /// </summary>
        public IList<ICommand>? Commands { get; set; }
    }
}
