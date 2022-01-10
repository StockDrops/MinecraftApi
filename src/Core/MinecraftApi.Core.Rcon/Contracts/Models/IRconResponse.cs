using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Contracts.Models
{
    /// <summary>
    /// A response given by the server.
    /// </summary>
    public interface IMinecraftResponseMessage
    {
        /// <summary>
        /// The request id of the response
        /// </summary>
        public int RequestId { get; }
        /// <summary>
        /// The body of the response. It shouldn't contain \0 bytes or other non Ascii elements.
        /// </summary>
        public string Body { get; }
        /// <summary>
        /// Raw body in bytes received.
        /// </summary>
        public byte[] RawBody { get; }
        /// <summary>
        /// Was the response successful.
        /// </summary>
        public bool IsSuccess { get; }
    }
}
