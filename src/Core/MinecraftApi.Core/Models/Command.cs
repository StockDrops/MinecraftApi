using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Command class for EF core.
    /// </summary>
    public class Command : ICommandEntity<Argument>
    {
        /// <summary>
        /// Arguments associated with the object
        /// </summary>
        public IList<Argument>? Arguments { get; set; }
        /// <summary>
        /// Associated plugin. Many commands - one plugin relationship.
        /// </summary>
        [JsonIgnore]
        public Plugin? Plugin { get; set; }
        /// <summary>
        /// Associated plugin Id. Many commands - one plugin relationship.
        /// </summary>
        public long PluginId { get; set; }
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public string? Description { get; set; }
        /// <inheritdoc/>
        public string? Prefix { get; set; }
        /// <inheritdoc/>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Command() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public Command(ICommandEntity<Argument> command)
        {
            Name = command.Name;
            Description = command.Description;
            Prefix = command.Prefix;
            Arguments = command.Arguments?.Select(x => new Argument(x)).ToList();
        }
    }
}
