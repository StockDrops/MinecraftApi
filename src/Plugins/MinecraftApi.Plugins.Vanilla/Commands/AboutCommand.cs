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
    public class AboutCommand : Command, ICommandEntity<SavedArgument>
    {
        public AboutCommand()
        {
            Id = 0;
            Name = "About";
            Description = "Returns information about the server";
            Prefix = "about";
            Arguments = new List<SavedArgument>()
                {
                    PluginArguments.PluginName
                };
        }
    }
}
