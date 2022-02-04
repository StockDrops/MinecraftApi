using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Commands
{
    /// <summary>
    /// An argument to use with a command
    /// </summary>
    public class RanArgument : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// Id of the associated argument in the database
        /// </summary>
        public long SavedArgumentId { get; set; }
        /// <summary>
        /// Saved argument in the database defining order.
        /// </summary>
        public SavedArgument? SavedArgument { get; set; } //these are already linked to a command internally.

        ///// <summary>
        ///// Id of the command ran 
        ///// </summary>
        //public long RanCommandId { get; set; }
        ///// <summary>
        ///// NAvigation property
        ///// </summary>
        //public RanCommand? RanCommand { get; set; }


        /// <summary>
        /// The value of the argument
        /// </summary>
        [Required]
        public string? Value { get; set; }
    }
}
