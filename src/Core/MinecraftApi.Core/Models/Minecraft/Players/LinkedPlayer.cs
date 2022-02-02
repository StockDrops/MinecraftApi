using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Minecraft.Players
{
    /// <summary>
    ///  A minecraft player linked to an Azure AD user id.
    /// </summary>
    public class LinkedPlayer : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }    
        /// <summary>
        /// MC player id
        /// </summary>
        /// 
        [Required]
        public string? PlayerId { get; set; }
        /// <summary>
        /// MC Player
        /// </summary>
        public MinecraftPlayer? Player { get; set; }
        /// <summary>
        /// The external id of the player.
        /// </summary>
        [Required]
        public string? ExternalId { get; set; }
    }
}
