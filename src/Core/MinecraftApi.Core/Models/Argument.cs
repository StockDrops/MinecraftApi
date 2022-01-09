using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Argument class for the 
    /// </summary>
    public class Argument : IArgumentEntity
    {
        /// <inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// Default empty argument.
        /// </summary>
        public Argument() { }
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public string? Description { get; set; }
        /// <inheritdoc/>
        public int? Order { get; set; }
        /// <inheritdoc/>
        public bool Required { get; set; }

        /// <summary>
        /// Build a concrete argument from an IArgument. Id is set to 0.
        /// </summary>
        /// <param name="argument"></param>
        public Argument(IBasicArgument argument)
        {
            Name = argument.Name;
            Description = argument.Description;
            Order = argument.Order;
            Id = 0;
        }
        #region EF Core Relationships

        /// <summary>
        /// Command associated with the argument.
        /// </summary>
        [JsonIgnore]
        public Command? Command { get; set; }
        /// <summary>
        /// Command Id associated with the argument.
        /// </summary>
        public long CommandId { get; set; }
        #endregion
    }
}
