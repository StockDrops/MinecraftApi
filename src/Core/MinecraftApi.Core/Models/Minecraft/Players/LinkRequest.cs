using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Minecraft.Players
{
    /// <summary>
    /// Defines a linking request for a given player.
    /// </summary>
    public class LinkRequest : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// Unique request id to send to the user.
        /// </summary>
        public string UniqueId { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Oauth2RequestUrl to use by the link request.
        /// </summary>
        public string Oauth2RequestUrl { get; set; } = "";
        /// <summary>
        /// Time when the link was requested.
        /// </summary>
        public DateTime RequestedTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// When the request link expires.
        /// </summary>
        public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddMinutes(60); //the linking default expiration.
        /// <summary>
        /// Player id.
        /// </summary>
        public string? PlayerId { get; set; }
        /// <summary>
        /// player to link.
        /// </summary>
        public MinecraftPlayer? Player { get; set; }
        /// <summary>
        /// Status of the request
        /// </summary>
        public LinkRequestStatus Status { get; set; } = LinkRequestStatus.Pending;
    }
}
