using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Integrations
{
    /// <summary>
    /// Represents a token used to access another API.
    /// </summary>
    public class Token : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// The access token created by the server.
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// The refresh token.
        /// </summary>
        /// 
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Date of the last time the token was generated.
        /// </summary>
        public DateTime AccessTokenGenerationDate { get; set; }
        /// <summary>
        /// Expiration in seconds of the token.
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Scope provided
        /// </summary>
        public string? Scope { get; set; }
        /// <summary>
        /// Type of token
        /// </summary>
        public TokenType TokenType { get; set; }
        /// <summary>
        /// Type of integration (Patreon or other).
        /// </summary>
        public IntegrationType IntegrationType { get; set; }
        /// <summary>
        /// The id of the linked MC player.
        /// </summary>
        public long LinkedPlayerId { get; set; }
        /// <summary>
        /// The navigational property to the linked player.
        /// </summary>
        [Required]
        public LinkedPlayer? LinkedPlayer { get; set; }
    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum IntegrationType
    {

        Patreon = 0,

    }
    public enum TokenType
    {
        User = 0,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
