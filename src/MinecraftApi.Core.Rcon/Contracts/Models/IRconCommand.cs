using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Contracts.Models
{
    /// <summary>
    /// Creates a command executes through RCON.
    /// </summary>
    public interface IRconCommand : IRconTextMessage
    {
        /// <summary>
        /// The command string to execute.
        /// </summary>
        public string Command { get; set; }
    }
}
