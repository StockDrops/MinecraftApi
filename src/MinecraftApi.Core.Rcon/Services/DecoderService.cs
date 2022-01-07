using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Services
{
    /// <summary>
    /// Class used to decode the rcon messages
    /// </summary>
    public class DecoderService
    {
        private const int HeaderLength = 10; // Does not include 4-byte message length.
        /// <summary>
        /// Decodes a raw message in bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static IRconMessage Decode(byte[] bytes)
        {
            int len = BitConverter.ToInt32(bytes, 0);
            int id = BitConverter.ToInt32(bytes, 4);
            int type = BitConverter.ToInt32(bytes, 8);

            int bodyLen = bytes.Length - (HeaderLength + 4);
            return new RconMessage
            {
                Body = bodyLen > 0 ? bytes.Skip(HeaderLength + 4).ToArray() : null, //TODO: probably use a faster method to get the body?
                RequestId = id,
                Type = (RconMessageType)type
            };
        }
    }
}
