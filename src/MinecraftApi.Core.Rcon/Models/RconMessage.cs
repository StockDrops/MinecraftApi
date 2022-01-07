using MinecraftApi.Core.Rcon.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Models
{
    /// <summary>
    /// Class defining a message used in the RCON protocol
    /// </summary>
    public class RconMessage : IRconMessage
    {
        ///<inheritdoc/>
        public int RequestId { get; set; }
        ///<inheritdoc/>
        public byte[]? Body { get; set; }
        ///<inheritdoc/>
        public RconMessageType Type { get; set; }
        ///<inheritdoc/>
        public int Length => Body?.Length ?? throw new InvalidOperationException("Cannot read length before setting the body of the message!");
    }
}
