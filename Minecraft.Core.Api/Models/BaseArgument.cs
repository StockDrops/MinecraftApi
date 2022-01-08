using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Models
{
    /// <summary>
    /// Defines the base argument of a command.
    /// </summary>
    public class BaseArgument : IArgument
    {
        ///<inheritdoc/>
        public string? Name { get; set; }
        ///<inheritdoc/>
        public string? Description { get; set; }
        ///<inheritdoc/>
        public string? Value { get; set; }
        ///<inheritdoc/>
        public int? Order { get; set; }
    }
}
