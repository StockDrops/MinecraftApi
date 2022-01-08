using MinecraftApi.Core.Rcon.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft.Rcon.Models
{
    /// <summary>
    /// Implements the IRconCommand to create commands through Rcon.
    /// </summary>
    public class RconCommand : RconTextMessage, IRconCommand
    {
        ///<inheritdoc/>
        public string Command { get => Text; set => Text = value; }
    }
}
