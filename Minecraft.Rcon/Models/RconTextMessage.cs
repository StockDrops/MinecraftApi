using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Rcon.Models
{
    ///<inheritdoc/>
    public class RconTextMessage : RconMessage, IRconTextMessage
    {
        private string _text = "";
        /// <summary>
        /// Default constructor
        /// </summary>
        public RconTextMessage()
        {
        }
        /// <summary>
        /// Generates a text message with a given text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="requestId"></param>
        /// <param name="messageType"></param>
        public RconTextMessage(string text, int requestId, RconMessageType messageType)
        {
            Text = text;
            RequestId = requestId;
            Type = messageType;
        }
        ///<inheritdoc/>
        public string Text 
        {
            get => _text; 
            set
            {
                _text = value;
                Body = Encoding.ASCII.GetBytes(_text);
            } 
        }
        
    }
}
