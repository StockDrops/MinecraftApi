using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Rcon.Models
{
    /// <summary>
    /// Implements the IRconCommand to create commands through Rcon.
    /// </summary>
    public class RconCommand : RconTextMessage, IRconCommand
    {
        /// <inheritdoc/>
        public RconCommand()
        {
        }
        /// Generate a command with a given text.
        public RconCommand(string command, int requestId) : base(command, requestId, RconMessageType.Command)
        {
        }
        ///<inheritdoc/>
        public string Command { get => Text; set => Text = value; }
        /// <inheritdoc/>
        public new RconMessageType Type => RconMessageType.Command;
    }
}
