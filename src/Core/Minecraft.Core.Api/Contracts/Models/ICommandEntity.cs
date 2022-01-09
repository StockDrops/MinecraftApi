using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// General command used on Minecraft that can be saved and tracked since it holds an Id.
    /// </summary>
    public interface ICommandEntity<T> : IEntity where T : IArgumentEntity
    {
        /// <summary>
        /// Name of the command. This is not the prefix used. It's a user given name used for easy reference.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Description of the command for documentation.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Command prefix. I.e. lp, gamemode, fly etc...
        /// </summary>
        public string? Prefix { get; set; }
        /// <summary>
        /// Arguments for the command in the order to be given.
        /// </summary>
        public IList<T>? Arguments { get; set; }
    }
}
