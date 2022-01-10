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
    public class SettableArgument : Argument, ISettableArgumentEntity
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
    }
}
