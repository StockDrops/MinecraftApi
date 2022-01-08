using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Models
{
    /// <summary>
    /// Base plugin for use with the rest of the code.
    /// </summary>
    public class BasePlugin : IPlugin
    {
        /// <inheritdoc/>
        public string? Name { get; set; }
        /// <inheritdoc/>
        public IList<ICommand>? Commands { get; set; }
    }
}
