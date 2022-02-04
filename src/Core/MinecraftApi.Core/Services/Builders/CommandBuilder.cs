using MinecraftApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MinecraftApi.Core.Services.Builders
{
    /// <summary>
    /// Creates a command
    /// </summary>
    public class CommandBuilder
    {
        private string prefix = "";
        
        private List<(int order, StringBuilder sb)> commandParts = new List<(int order, StringBuilder sb)>();
        /// <summary>
        /// Builds a command
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            var orderedParts = commandParts.OrderBy(c => c.order);
            var arguments = string.Join(" ", orderedParts.Select(c => c.sb.ToString()));
            return $"{prefix} {arguments}";
        }

        /// <summary>
        /// Sets a prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public CommandBuilder SetPrefix(string prefix)
        {
            this.prefix = prefix;
            return this;
        }
        /// <summary>
        /// Adds an argument to the command
        /// </summary>
        /// <param name="value"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public CommandBuilder AddArgument(string value, int order = 0)
        {
            commandParts.Add((order, new StringBuilder(value)));
            return this;
        }
        //public CommandBuilder FromSaved(Command command)
        //{
        //    SetPrefix(command.Prefix);
        //    if(command.Arguments == null)
        //        throw new ArgumentNullException(nameof(command.Arguments));
        //    //let's order the arguments
        //    var orderedArguments = command.Arguments.OrderBy(a => a.Order);

        //    foreach(var argument in command.Arguments)
        //    {

        //    }
        //}
    }
}
