using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Helpers;
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
        private byte[]? _requestId = null;
        private int? Length
        {
            get => Body?.Length + _requestId?.Length + _type?.Length + 2; //+2 because the terminator is 2 bytes long.
        }
        ///<inheritdoc/>
        public int RequestId
        {
            get { return BitConverter.ToInt32(_requestId); }
            set
            {
                _requestId = BitConverter.GetBytes(value);
            }
        }
        ///<inheritdoc/>
        public byte[]? Body { get; set; }

        private byte[]? _type = null;
        ///<inheritdoc/>
        public RconMessageType Type
        {
            get
            {
                if (_type != null)
                    return (RconMessageType)BitConverter.ToInt32(_type);
                return RconMessageType.EmptyMessage;
            }
            set
            {
                _type = BitConverter.GetBytes((int)value);
            }
        }
        ///<inheritdoc/>
        public byte[]? Header
        {
            get
            {
                var length = Length;
                if (length != null)
                {
                    if (_requestId != null && _type != null && Body != null)
                        return ByteArrayHelpers.Combine(BitConverter.GetBytes(length.Value), _requestId, _type);
                }
                return null;
            }
        }
        ///<inheritdoc/>
        public byte[]? RawMessage 
        {
            get
            {
                if(Body != null && Header != null)
                    return ByteArrayHelpers.Combine(Header, Body, new byte[] { 0, 0 });
                return null;
            }
        }
    }
}
