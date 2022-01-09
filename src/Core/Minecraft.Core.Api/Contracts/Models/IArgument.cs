using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// Base argument
    /// </summary>
    public interface IBasicArgument
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
        /// Optional argument defining the order of the argument.
        /// If not given the order of the list of arguments will be used.
        /// It needs to be given for ALL THE ARGUMENTS of a command.
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// Is the argument required?
        /// </summary>
        public bool Required { get; set; }
    }
    /// <summary>
    /// And argument used by a plugin
    /// </summary>
    public interface ISettableArgument : IBasicArgument
    {
        /// <summary>
        /// Value of the argument.
        /// </summary>
        public string? Value { get; set; }
    }
    /// <summary>
    /// An argument with a value property that can be tracked and saved thanks to being an entity (has an ID).
    /// </summary>
    public interface ISettableArgumentEntity : ISettableArgument, IEntity
    {

    }
    /// <summary>
    /// A basic argument that can serve as an entity thanks to inheriting from IEntity.
    /// </summary>
    public interface IArgumentEntity : IBasicArgument, IEntity
    {

    }
}
