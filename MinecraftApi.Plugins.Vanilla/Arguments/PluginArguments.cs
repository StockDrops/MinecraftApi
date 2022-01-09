using MinecraftApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Plugins.Vanilla.Arguments
{
    public static class PluginArguments
    {
        public static Argument PluginName = new Argument { Name = "Plugin Name", Description = "Provide the name of the plugin you want information about.", Order = 0, Required = false };
        public static Argument Username = new Argument { Name = "Username", Description = "Provide a username.", Order = 0, Required = true };
        public static Argument GameMode = new Argument { Name = "Game Mode", Description = "Provide a game mode.", Order = 0, Required = true };
    }
}
