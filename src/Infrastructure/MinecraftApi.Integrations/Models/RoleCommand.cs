using MinecraftApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Models
{
    public static class RoleCommands
    {
        
        public static Command RoleCommand = new Command
        {
            Name = "RoleCommand",
            Description = "Sets the role of a new user",
            Prefix = "lp user",
            Arguments = new List<SavedArgument>
            {
                new SavedArgument { Name = "User", Description = "Enter the username or user id", Order = 0, Required = true },
                new SavedArgument { Name = "Group", Description = "Group to modify", DefaultValue = "parent", Order = 1, Required = false },
                new SavedArgument { Name = "Action", Description = "What to do?", DefaultValue = "add", Order = 2, Required = false },
                new SavedArgument { Name = "Role", Description = "The role/group to affect.", Order = 3, Required = true }
            }
        };

        public static Plugin LuckyPermsPlugin = new Plugin { Name = "LuckyPerms", Commands = new List<Command> 
        {
            RoleCommand,
        } 
        };

    }
}
