using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Ef.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Ef.Models
{
    /// <summary>
    /// Argument class for the 
    /// </summary>
    public class Argument : IEntity, IArgument
    {
        /// <inheritdoc/>
        public long Id { get; set; }
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public string? Description { get; set; }
        /// <inheritdoc/>
        public string? Value { get; set; }
        /// <inheritdoc/>
        public int? Order { get; set; }
        
    }
}
