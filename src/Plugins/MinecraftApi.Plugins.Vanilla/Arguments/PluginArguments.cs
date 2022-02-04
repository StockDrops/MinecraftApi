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
        public static SavedArgument PluginName = new SavedArgument { Name = "Plugin Name", Description = "Provide the name of the plugin you want information about.", Order = 0, Required = false };
        public static SavedArgument Username = new SavedArgument { Name = "Username", Description = "Provide a username.", Order = 0, Required = true };
        public static SavedArgument GameMode = new SavedArgument { Name = "Game Mode", Description = "Provide a game mode.", Order = 0, Required = true };
    }
}
