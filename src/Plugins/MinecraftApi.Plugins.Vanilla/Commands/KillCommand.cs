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
    public class KillCommand : Command, ICommandEntity<SavedArgument>
    {
        public KillCommand()
        {
            Id = 0;
            Name = "Kill";
            Description = "Kills a given player.";
            Prefix = "kill";
            Arguments = new List<SavedArgument>()
        {
            PluginArguments.Username
        };
        }
       

    }
}
