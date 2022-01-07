using MinecraftApi.Core.Rcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Contracts.Models
{
    /// <summary>
    /// Rcon Message Template. This is the RAW message.
    /// </summary>
    public interface IRconMessage
    {
        /// <summary>
        /// Request Id set by the client to identify the messages.
        /// </summary>
        public int RequestId { get; set; }
        /// <summary>
        /// Body of the message.
        /// </summary>
        public byte[]? Body { get; set; }
        /// <summary>
        /// Type of the message. See <see cref="RconMessageType"/>.
        /// </summary>
        public RconMessageType Type { get; set; }
        /// <summary>
        /// Length of the message.
        /// </summary>
        public int Length { get; }
    }
}
