using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Models
{
    public class RconMessage
    {
        public int RequestId { get; set; }
        public string? Body { get; set; }
        public RconMessageType Type { get; set; }
        public int Length { get; }
    }
}
