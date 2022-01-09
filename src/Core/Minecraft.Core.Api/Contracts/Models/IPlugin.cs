using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// Defines a plugin used on Minecraft. Could this be simplified?
    /// </summary>
    public interface IPlugin<TCommand, TArgument>
        where TCommand : ICommandEntity<TArgument>
        where TArgument : IArgumentEntity
    {
        /// <summary>
        /// Id of the plugin.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name of the plugin. It must be unique! It identifies the plugin.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Commands defined for the plugin.
        /// </summary>
        public IList<TCommand>? Commands { get; set; }
    }
}
