using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Ef.Models;
using MinecraftApi.Integrations.Contracts.Patreon;
using MinecraftApi.Integrations.Models.Legacy;
using MinecraftApi.Integrations.Patreon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Services
{
    public class PlayerLinkingService
    {
        private readonly ICommandExecutionService commandExecutionService;
        private readonly IPatreonService patreonService;
        private readonly IDbContextFactory<PluginContext> pluginContextFactory;
        private readonly LegacyApiService legacyApiService;
        public PlayerLinkingService(ICommandExecutionService commandExecutionService,
            IPatreonService patreonService,
            LegacyApiService legacyApiService,
            IDbContextFactory<PluginContext> pluginContextFactory)
        {
            this.commandExecutionService = commandExecutionService;
            this.pluginContextFactory = pluginContextFactory;
            this.patreonService = patreonService;
            this.legacyApiService = legacyApiService;
        }


        public async Task<(bool success, string message)> LinkPatreonAccountAsync(string requestId, string userId)
        {

        }

        /// <summary>
        /// Links a legacy account.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(bool success, string message)> LinkLegacyAccountAsync(string requestId, string userId)
        {
            var success = false;
            var message = "";
            try
            {
                var player = await patreonService.LinkPlayerAsync(requestId, userId);
                if (player != null && player.ExternalId != null)
                {
                    var subscription = await legacyApiService.GetSubscription(player.ExternalId);
                    if (subscription != null && subscription.Name != null)
                    {
                        await LinkPlayer(userId, player.Id, subscription.Name);
                    }
                    success = true;
                }
            }
            catch (LinkRequestNotFound)
            {
                message = "The linking request has either expired, doesn't exist, or has already being used. Please create a new one on the Minecraft server.";
                success = false;
            }
            return (success, message);
        }



        private async Task LinkPlayer(string userId, long linkedPlayerId, string role)
        {
            using var pluginContext = pluginContextFactory.CreateDbContext();
            var linkedPlayer = await pluginContext.LinkedPlayers.Include(l => l.Player).FirstOrDefaultAsync(l => l.Id == linkedPlayerId);
            if (linkedPlayer == null)
                throw new ArgumentOutOfRangeException("Linked player doesn't exist");
            if (linkedPlayer.ExternalId != userId)
                throw new InvalidOperationException("Cannot link a user id with a different player.");


            var roleLevel = (RoleLevel)Enum.Parse(typeof(RoleLevel), role);
            var roleId = await pluginContext.Roles.Where(r => r.Level == roleLevel).Select(r => r.Id).FirstAsync();

            var response = await commandExecutionService.ExecuteAsync($"lp user {linkedPlayer.Player!.Id} parent add {roleLevel}");
            if (response.IsSuccess)
            {
                var linkedPlayerRole = new LinkedPlayerRole
                {
                    AssignedOn = DateTime.UtcNow,
                    CheckRoleBy = DateTime.UtcNow.AddDays(15),
                    LinkedPlayerId = linkedPlayerId,
                    RoleId = roleId
                };
                pluginContext.LinkedPlayerRoles.Add(linkedPlayerRole);
                await pluginContext.SaveChangesAsync();
                return;
            }
            throw new InvalidOperationException(response.Body);
        }
    }

}
