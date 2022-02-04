using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Core.Services.Builders;
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

        /// <summary>
        /// Links a patreon account to a role in minecraft.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<(bool success, string message)> LinkPatreonAccountAsync(string requestId, string userId, string code)
        {
            if(string.IsNullOrEmpty(requestId))
                throw new ArgumentNullException(nameof(requestId));
            if(string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            if(string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));
            var message = string.Empty;
            var success = false;

            try
            {
                var linkedPlayer = await patreonService.VerifyCodeAsync(code, requestId, userId);
                var role = await patreonService.GetTiersAsync(linkedPlayer.Id);
                if(role != null)
                {
                    if(await AwardRoleAsync(userId, linkedPlayer, role))
                    {
                        using var context = pluginContextFactory.CreateDbContext();
                        success = true;
                        var player = context.MinecraftPlayers.Where(p => p.Id == linkedPlayer.PlayerId).FirstOrDefault();
                        if(player != null)
                        {
                            message = $"{role.Level} was awarded succesfully to {player.PlayerName}";
                            return (success, message);
                        }
                    }
                    message = "Failed to award role.";
                    return (success, message);
                }
                message = "Couldn't find role to award";
            }
            catch (LinkRequestNotFound ex)
            {
                message = ex.Message;
                success = false;
            }
                        
                    
                
            return (success, message);
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
                        if(await LinkPlayer(userId, player.Id, subscription.Name))
                        {
                            using var context = pluginContextFactory.CreateDbContext();
                            success = true;
                            var mcplayer = context.MinecraftPlayers.Where(p => p.Id == player.PlayerId).FirstOrDefault();
                            if(mcplayer != null)
                                message = $"{subscription.Name} was awarded succesfully to {mcplayer.PlayerName}";
                            return (success, message);
                        }
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

        private async Task<bool> AwardRoleAsync(string userId, LinkedPlayer player, Role role)
        {
            using var pluginContext = pluginContextFactory.CreateDbContext();
            if (userId != player.ExternalId)
                throw new InvalidOperationException("You can only award a role to the same player/user id as yours");

            var command = new CommandBuilder()
                .SetPrefix("lp users")
                .AddArgument(player.PlayerId)
                .AddArgument("parent")
                .AddArgument("add")
                .AddArgument(role.Level.ToString())
                .Build();

            var response = await commandExecutionService.ExecuteAsync(command, userId);
            if (response.IsSuccess)
            {
                var linkedPlayerRole = new LinkedPlayerRole
                {
                    AssignedOn = DateTime.UtcNow,
                    CheckRoleBy = DateTime.UtcNow.AddDays(15),
                    LinkedPlayerId = player.Id,
                    RoleId = role.Id
                };
                pluginContext.LinkedPlayerRoles.Add(linkedPlayerRole);
                await pluginContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> LinkPlayer(string userId, long linkedPlayerId, string role)
        {
            using var pluginContext = pluginContextFactory.CreateDbContext();
            var linkedPlayer = await pluginContext.LinkedPlayers.Include(l => l.Player).FirstOrDefaultAsync(l => l.Id == linkedPlayerId);
            if (linkedPlayer == null)
                throw new ArgumentOutOfRangeException("Linked player doesn't exist");
            if (linkedPlayer.ExternalId != userId)
                throw new InvalidOperationException("Cannot link a user id with a different player.");


            var roleLevel = (RoleLevel)Enum.Parse(typeof(RoleLevel), role);
            var roleToAward = await pluginContext.Roles.Where(r => r.Level == roleLevel).FirstAsync();

            return await AwardRoleAsync(userId, linkedPlayer, roleToAward);
        }
    }

}
