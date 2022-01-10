using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Extensions
{
    /// <summary>
    /// Extensions for commands
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// validates the validity of the command, specially that a prefix exists.
        /// </summary>
        /// <returns></returns>
        public static bool ValidateCommand<T>(this ICommandEntity<T> command) where T : ISettableArgumentEntity
        {
            if(command.Arguments == null)
            {
                return false;
            }
            //we check that the arguments are set and that the prefix is not null or empty since that's what we need for sending a command.
            return !string.IsNullOrEmpty(command.Prefix);
        }
        /// <summary>
        /// Check arguments of the ICommandEntity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settableArguments"></param>
        /// <returns></returns>
        public static bool CheckArguments<T>(this IEnumerable<T> settableArguments) where T : ISettableArgumentEntity
        {
            //checks if the arguments are set
            if (settableArguments.Any())
            {
                return settableArguments.All(a => !string.IsNullOrEmpty(a.Value));
            }
            else
                return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arguments"></param>
        /// <param name="settableArguments"></param>
        public static void SetArguments<T>(this IEnumerable<T> arguments, IEnumerable<T> settableArguments) where T: ISettableArgumentEntity
        {
            if (!settableArguments.Any())
            {
                throw new ArgumentException($"{nameof(settableArguments)} cannot be empty");
            }
            foreach(var argument in arguments)
            {
                argument.Value = settableArguments.Where(s => s.Id == argument.Id).Select(s => s.Value).First();
            }
        }
        /// <summary>
        /// Copy a command information from a saved entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="savedCommand"></param>
        public static void CopyFrom<T>(this ICommandEntity<T> command, Command savedCommand) where T: ISettableArgumentEntity
        {
            command.Id = savedCommand.Id;
            command.Name = savedCommand.Name;
            command.Prefix = savedCommand.Prefix;
            command.Description = savedCommand.Description;
            command.PluginId = savedCommand.PluginId;
            if(command.Arguments != null && command.Arguments.Any())
            {
                //order the saved arguments:
                if (savedCommand.Arguments != null)
                {
                    var orderedArguments = savedCommand.Arguments.OrderBy(a => a.Order).ToList();
                    for (int i = 0; i < command.Arguments.Count; i++)
                    {
                        if (savedCommand.Arguments != null && orderedArguments.Count > i && command.Arguments[i].Id != 0)
                        {
                            var savedArgument = orderedArguments.Where(o => o.Id == command.Arguments[i].Id).First();
                            command.Arguments[i].Order = savedArgument.Order;
                            command.Arguments[i].Name = savedArgument.Name;
                            command.Arguments[i].Description = savedArgument.Description;
                        }
                        //no id given do nothing.
                    }
                }
                
            }
            
        }
    }
}
