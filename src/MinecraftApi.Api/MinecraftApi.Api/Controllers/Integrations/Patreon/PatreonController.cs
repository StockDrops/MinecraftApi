using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Integrations.Contracts.Patreon;

namespace MinecraftApi.Api.Controllers.Integrations.Patreon
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatreonController : ControllerBase
    {
        private readonly IPatreonService patreonService;
        ///<inheritdoc/>
        public PatreonController(IPatreonService patreonService)
        {
            this.patreonService = patreonService;
        }
        /// <summary>
        /// Links a request
        /// </summary>
        /// <param name="player"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// 
        [HttpPost("link")]
        [Authorize]
        public async Task<ActionResult<LinkRequest>> CreateLinkRequestMinecraft([FromBody] MinecraftPlayer player, CancellationToken token = default)
        {
            try
            {
                var request = await patreonService.CreateLinkRequestAsync(player, token);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Verify link request.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="requestId"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("verify")]
        [Authorize]
        public async Task<ActionResult<LinkResponse>> VerifyLinkRequest([FromQuery] string requestId, [FromQuery] string externalId, [FromQuery] string? code = null)
        {
            var response = new LinkResponse();
            try
            {
                if(code != null)
                    response.IsSuccess = await patreonService.VerifyCodeAsync(code, requestId, externalId);
                else
                {
                    var linkedPlayer = await patreonService.LinkPlayerAsync(requestId, externalId);
                    if (linkedPlayer != null)
                        response.IsSuccess = true;
                    else
                        Problem("Linking failed");
                }
                    
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return Problem(response.Message);
            }
            return Ok(response);
        }
    }
}
