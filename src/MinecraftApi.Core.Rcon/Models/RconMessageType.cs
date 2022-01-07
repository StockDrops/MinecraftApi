using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Models
{
    public enum RconMessageType
    {
        Response = 0,
        Command = 2,
        Authenticate = 3
    }
}
