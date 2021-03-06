using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftApi.Rcon.Models
{
    /// <summary>
    /// An implementation of IRconResponseMessage
    /// </summary>
    public class MinecraftResponseMessage : IMinecraftResponseMessage
    {
        ///<inheritdoc/>
        public int RequestId { get; }
        ///<inheritdoc/>
        public string Body { get; }
        ///<inheritdoc/>
        public byte[] RawBody { get; }
        ///<inheritdoc/>
        public bool IsSuccess { get; }
        /// <summary>
        /// Creates a response message.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="rawbody"></param>
        /// <param name="isSuccess"></param>
        public MinecraftResponseMessage(int requestId, byte[] rawbody, bool isSuccess)
        {
            RequestId = requestId;
            RawBody = rawbody;
            Body = Encoding.UTF8.GetString(rawbody).Trim('\0').StripColorCodes();
            IsSuccess = isSuccess;
        }
        /// <summary>
        /// Create an empty response
        /// </summary>
        /// <param name="message"></param>
        /// <param name="requestId"></param>
        /// <param name="isSuccess"></param>
        public MinecraftResponseMessage(string message = "", int requestId = -1, bool isSuccess = false)
        {
            RequestId = requestId;
            RawBody = new byte[0];
            Body = message;
            IsSuccess = isSuccess;
        }

    }
}
