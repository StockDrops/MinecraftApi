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
    public interface IRconTextMessage : IRconMessage
    {
        /// <summary>
        /// The text string to send through rcon.
        /// </summary>
        public string Text { get; set; }
    }
}
