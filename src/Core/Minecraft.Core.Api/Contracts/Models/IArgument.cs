using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// And argument used by a plugin
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// Name of the argument
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Description for the argument.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Value of the argument.
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// Optional argument defining the order of the argument.
        /// If not given the order of the list of arguments will be used.
        /// It needs to be given for ALL THE ARGUMENTS of a command.
        /// </summary>
        public int? Order { get; set; }
    }
}
