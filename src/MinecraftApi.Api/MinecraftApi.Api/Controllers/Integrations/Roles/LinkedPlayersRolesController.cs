using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Api.Controllers.Base;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;

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
        ///<inheritdoc/>
        public LinkedPlayersRolesController(ICommandExecutionService commandExecutionService,
            IRepositoryService<LinkedPlayerRole> repositoryService,
            ILogger<CrudBaseController<LinkedPlayerRole>> logger) : base(repositoryService, logger)
        {
            this.commandExecutionService = commandExecutionService;
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
        public override Task<IActionResult> UpdateAsync(long id, [FromBody] LinkedPlayerRole entity)
        {
            return base.UpdateAsync(id, entity);
        }
    }
}
