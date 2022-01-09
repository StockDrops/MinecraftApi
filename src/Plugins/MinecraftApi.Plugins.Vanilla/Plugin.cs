using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Plugins.Vanilla.Commands;

namespace MinecraftApi.Plugins.Vanilla
{
    public class MinecraftMainPlugin : Plugin
    {
        public MinecraftMainPlugin() : base()
        {
            Id = 0;
            Name = "Vanilla";
            Commands = new List<Command>()
                {
                    new AboutCommand(),
                    new KillCommand(),
                    new GameModeCommand()
                };
        }
    }
    
}