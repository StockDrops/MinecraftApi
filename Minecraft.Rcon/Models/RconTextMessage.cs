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
