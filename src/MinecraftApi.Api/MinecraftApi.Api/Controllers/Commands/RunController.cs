using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;
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
        private readonly ICommandExecutionService _commandExecutionService;
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="commandExecutionService"></param>
        public RunController(ICommandExecutionService commandExecutionService)
        {
            _commandExecutionService = commandExecutionService;
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
            var response = await _commandExecutionService.ExecuteAsync(command, token);
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
            var response = await _commandExecutionService.ExecuteAsync(command, token);
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
