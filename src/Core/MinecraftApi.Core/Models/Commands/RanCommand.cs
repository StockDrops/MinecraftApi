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
    /// A command ran, used with raw commands sent to the RCON.
    /// </summary>
    public class BaseRanCommand : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// The date time the command was run in UTC.
        /// </summary>
        public DateTime RanTime { get; set; }
        /// <summary>
        /// Raw Command sent
        /// </summary>
        /// 
        [Required]
        public string? RawCommand { get; set; }
        /// <summary>
        /// The user id of the person that ran the command.
        /// </summary>
        public string? UserId { get; set; }
    }
    /// <summary>
    /// A command that was run with more details, used with complex commands
    /// </summary>
    public class RanCommand : BaseRanCommand, IEntity
    {
        /// <summary>
        /// The command id
        /// </summary>
        public long CommandId { get; set; }
        /// <summary>
        /// The command ran.
        /// </summary>
        public Command? Command { get; set; }
        /// <summary>
        /// Arguments that were sent with the command.
        /// </summary>
        public IList<RanArgument> RanArguments { get; set; } = new List<RanArgument>();
         
    }
}
