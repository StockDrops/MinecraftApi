using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Ef.Models;
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
        private readonly IDbContextFactory<PluginContext> pluginContextFactory;
        public PlayerLinkingService(ICommandExecutionService commandExecutionService,
            IDbContextFactory<PluginContext> pluginContextFactory)
        {
            this.commandExecutionService = commandExecutionService;
            this.pluginContextFactory = pluginContextFactory;
        }

        public async Task LinkPlayer(string userId, long linkedPlayerId, string role)
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
