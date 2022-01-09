using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Plugins.Vanilla.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Plugins.Vanilla.Commands
{
    public class GameModeCommand : Command, ICommandEntity<Argument>
    {
        public GameModeCommand()
        {
            Id = 0;
            Name = "Game Mode";
            Description = "Sets a given player gamemode";
            Prefix = "gamemode";
            Arguments = new List<Argument>()
            {
                PluginArguments.GameMode
            };
        }
    }
}
