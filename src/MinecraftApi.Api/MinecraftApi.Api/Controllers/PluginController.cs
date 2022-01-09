using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Ef.Services;

namespace MinecraftApi.Api.Controllers
{
    //[Authorize] //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]


    /// <summary>
    /// Plugin controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PluginController : ControllerBase
    {
        private readonly ILogger<PluginController> _logger;
        private readonly PluginService _pluginService;
        /// <summary>
        /// Creates the plugin controller.
        /// </summary>
        /// <param name="pluginService"></param>
        /// <param name="logger"></param>
        public PluginController(PluginService pluginService, ILogger<PluginController> logger)
        {
            _pluginService = pluginService;
            _logger = logger;
        }
        /// <summary>
        /// Gets a plugin.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetPlugin")]
        public async Task<IPlugin<Command, Argument>> Get(long id)
        {
            var obj =  await _pluginService.RetrievePluginAsync(id);
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        [HttpPost(Name = "SavePlugin")]
        public async Task<ActionResult> SavePlugin([FromBody] Plugin plugin)
        {
            await _pluginService.SaveAsync(plugin);
            return Ok();
        }
    }
}
