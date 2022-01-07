using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Models
{
    /// <summary>
    /// Types of messages used in RCON protocol.
    /// We added the "empty message" for usage as a default value.
    /// </summary>
    public enum RconMessageType
    {
        /// <summary>
        /// Response message from the server
        /// </summary>
        Response = 0,
        /// <summary>
        /// A command
        /// </summary>
        Command = 2,
        /// <summary>
        /// Login message.
        /// </summary>
        Authenticate = 3,
        /// <summary>
        /// Empty message, used for default type values. If this is left as default we know when the message is badly set up.
        /// </summary>
        EmptyMessage = 4
    }
}
