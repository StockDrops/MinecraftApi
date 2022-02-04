using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Represents a linked player that has been assigned a role
    /// </summary>
    public class LinkedPlayerRole : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// Date of role assignement
        /// </summary>
        public DateTime AssignedOn { get; set; }
        /// <summary>
        /// When should this role be rechecked and made sure that it's still valid for the existing player? Based on the monthly expiration of pledges.
        /// </summary>
        public DateTime CheckRoleBy { get; set; }
        /// <summary>
        /// The linked player id
        /// </summary>
        public long LinkedPlayerId { get; set; }
        /// <summary>
        /// The linked player navigation property
        /// </summary>
        public LinkedPlayer? LinkedPlayer { get; set; }
        /// <summary>
        /// The id of the given role
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// The role's navigation property.
        /// </summary>
        public Role? Role { get; set; }
    }
}
