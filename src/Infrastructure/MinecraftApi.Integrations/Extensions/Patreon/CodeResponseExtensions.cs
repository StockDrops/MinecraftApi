using MinecraftApi.Core.Models.Integrations;
using MinecraftApi.Integrations.Models.Patreon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Extensions.Patreon
{
    /// <summary>
    /// Extension class for code responses
    /// </summary>
    public static class CodeResponseExtensions
    {
        public static Token ToToken(this CodeResponse codeResponse, long linkedPlayerId)
        {
            return new Token
            {
                AccessToken = codeResponse.AccessToken,
                RefreshToken = codeResponse.RefreshToken,
                AccessTokenGenerationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddSeconds(codeResponse.ExpiresIn - 1),
                IntegrationType = IntegrationType.Patreon,
                TokenType = TokenType.User,
                LinkedPlayerId = linkedPlayerId,
                Scope = codeResponse.Scope

            };
        }
    }
}
