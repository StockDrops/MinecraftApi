using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Ef.Models;
using OpenStockApi.Core.Models.Configuration;

namespace MinecraftApi.Api.Controllers
{
    /// <summary>
    /// Runs commands
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequiredScope(ApiScopes.Run)]
    public class RunController : ControllerBase
    {
        private readonly ICommandExecutionService commandExecutionService;
        private readonly PluginContext pluginContext;
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="pluginContext"></param>
        /// <param name="commandExecutionService"></param>
        public RunController(PluginContext pluginContext,
            ICommandExecutionService commandExecutionService)
        {
            this.commandExecutionService = commandExecutionService;
            this.pluginContext = pluginContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IMinecraftResponseMessage> RunCommand([FromBody]Command<SettableArgument> command, CancellationToken token)
        {
            var userId = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Select(c => c.Value).FirstOrDefault();
            var response = await commandExecutionService.ExecuteAsync(command, userId: userId, token: token);
            if (response.IsSuccess)
            {
                Ok();
                return response;
            }
            Problem();
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandId"></param>
        /// <param name="arguments"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{commandId}")]
        public async Task<IMinecraftResponseMessage> RunCommand(long commandId, [FromBody]List<RanArgument> arguments, CancellationToken token)
        {
            var userId = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Select(c => c.Value).FirstOrDefault();

            var response = await commandExecutionService.ExecuteAsync(commandId, arguments, userId, token);
            if (response.IsSuccess)
            {
                Ok();
                return response;
            }
            Problem();
            return response;
        }
        /// <summary>
        /// Runs a raw command. No complex sanity checks will be done here. Just directly forward the command to RCON.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("raw")]
        public async Task<IMinecraftResponseMessage> RunCommandAsync(string command, CancellationToken token)
        {
            var userId = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Select(c => c.Value).FirstOrDefault();
            var response = await commandExecutionService.ExecuteAsync(command, userId: userId, token: token);
            if (response.IsSuccess)
            {
                Ok();
                return response;
            }
            Problem();
            return response;
        }
    }
}
