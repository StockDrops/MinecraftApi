﻿using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;

namespace MinecraftApi.Integrations.Contracts.Patreon;

/// <summary>
/// The patreon service template. It should be able to verify the code and link it to a link request.
/// </summary>
public interface IPatreonService
{
    /// <summary>
    /// Create a link request between a minecraft player and a patreon account.
    /// </summary>
    /// <param name="minecraftPlayer"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<LinkRequest> CreateLinkRequestAsync(MinecraftPlayer minecraftPlayer, CancellationToken token = default);
    /// <summary>
    /// Verifies a code with Patreon API
    /// </summary>
    /// <param name="code"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> VerifyCodeAsync(string code, string azureId, string uniqueRequestId, CancellationToken token = default);
    /// <summary>
    /// Links a minecraft player to an external id.
    /// </summary>
    /// <param name="uniqueRequestId"></param>
    /// <param name="externalId"></param>
    /// <param name="setRequestAsCompleted"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="LinkRequestNotFound"></exception>
    Task<LinkedPlayer> LinkPlayerAsync(string uniqueRequestId, string externalId, bool setRequestAsCompleted = true, CancellationToken token = default);
}
