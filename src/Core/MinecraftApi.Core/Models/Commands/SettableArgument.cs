using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// Argument with Value property so it can be set to a given value.
    ///<inheritdoc/>
    public class SettableArgument : SavedArgument, ISettableArgumentEntity
    {
        /// <summary>
        /// Constructs a settable argument.
        /// </summary>
        /// <param name="value"></param>
        public SettableArgument(string value)
        {
            Value = value;
        }
        /// <summary>
        /// Creates an empty settable argument.
        /// </summary>
        public SettableArgument()
        {
            Value = "";
        }

        /// <inheritdoc/>
        public string Value { get; set; }
        ///<inheritdoc/>
        public override string ToString()
        {
            return Value;
        }
        /// <summary>
        /// Copy constructed.
        /// </summary>
        /// <param name="argument"></param>
        public SettableArgument(SavedArgument argument)
        {
            Value = "";
            Name = argument.Name;
            Description = argument.Description;
            CommandId = argument.CommandId;
            Name = argument.Name;
            Order = argument.Order;
            Required = argument.Required;
            Id = argument.Id;
        }
    }
}
