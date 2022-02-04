using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Api.Controllers.Base;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Ef.Models;
using OpenStockApi.Core.Models.Configuration;

namespace MinecraftApi.Api.Controllers.Integrations.Roles
{
    /// <summary>
    /// This controller will be in charge of assigning roles to users.
    /// </summary>
    /// 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LinkedPlayersRolesController : CrudBaseController<LinkedPlayerRole>
    {
        private readonly ICommandExecutionService commandExecutionService;
        private readonly IRepositoryService<LinkedPlayer> linkedPlayerRepository;
        private readonly PluginContext pluginContext;
        ///<inheritdoc/>
        public LinkedPlayersRolesController(ICommandExecutionService commandExecutionService,
            IRepositoryService<LinkedPlayerRole> repositoryService,
            IRepositoryService<LinkedPlayer> linkedPlayerRepository,
            PluginContext pluginContext,
            ILogger<CrudBaseController<LinkedPlayerRole>> logger) : base(repositoryService, logger)
        {
            this.commandExecutionService = commandExecutionService;
            this.linkedPlayerRepository = linkedPlayerRepository;
            this.pluginContext = pluginContext;
        }
        ///<inheritdoc/>
        public override Task<ActionResult<LinkedPlayerRole>> SaveAsync([FromBody] LinkedPlayerRole entity)
        {
            return base.SaveAsync(entity);
        }
        /// <summary>
        /// Updates a linked player to its role. It will also update it on the minecraft server by running the required command.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        
        public override Task<IActionResult> UpdateAsync(long id, [FromBody] LinkedPlayerRole entity)
        {
            return base.UpdateAsync(id, entity);
        }
        /// <summary>
        /// links a player to a role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="linkedPlayerId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("{userId}/{linkedPlayerId}/{role}")]
        [RequiredScope(ApiScopes.LinkAsUser)]
        public async Task<ActionResult> LinkPlayer(string userId, long linkedPlayerId, string role)
        {
            var uid = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Select(c => c.Value).FirstOrDefault();
            if (uid != userId)
            {
                return Unauthorized();
            }

            var linkedPlayer = await pluginContext.LinkedPlayers.Include(l => l.Player).FirstOrDefaultAsync(l => l.Id == linkedPlayerId);
            if (linkedPlayer == null)
                return Problem("Linked player doesn't exist");
            try
            {
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
                    return Ok();
                }
                return Problem(response.Body);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }
    }
}
