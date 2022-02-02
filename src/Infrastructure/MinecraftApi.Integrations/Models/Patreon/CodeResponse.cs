using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Models.Patreon
{
    /// <summary>
    /// Defines a response by Patreon's oauth2 endpoint when verifying a code.
    /// </summary>
    public class CodeResponse
    {
        /// <summary>
        /// The access token created by the server.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        /// <summary>
        /// The refresh token.
        /// </summary>
        /// 
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Expiration in seconds of the token.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Scope provided
        /// </summary>
        /// 
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
        /// <summary>
        /// Type of token
        /// </summary>
        /// 
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
    }
}
