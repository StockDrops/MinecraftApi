using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Ef.Services;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinecraftApi.Api.Controllers
{
    /// <summary>
    /// Manages reception of commands.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ILogger<CommandController> _logger;
        private readonly CommandService _commandService;
        /// <summary>
        /// Construct a command controller.
        /// </summary>
        /// <param name="commandService"></param>
        /// <param name="logger"></param>
        public CommandController(CommandService commandService, ILogger<CommandController> logger)
        {
            _logger = logger;
            _commandService = commandService;
        }
        /// <summary>
        /// GET: api/Command/plugin/id
        /// Gets all the commands associated with a given plugin.
        /// </summary>
        /// 
        /// 
        [HttpGet("plugin/{id}")]
        public IEnumerable<string> Get(long id)
        {
            return new string[] { "value1", "value2" };
        }

        /// GET api/<CommandController>/5
        [HttpGet("{id}")]
        public async Task<ICommandEntity<Argument>?> GetCommand(long id, CancellationToken token)
        {
            var result = await _commandService.RetrieveCommandAsync(id, token);
            if(result != null)
            {
                return result;
            }
            NotFound(result);
            return null;
        }

        /// POST api/<CommandController>
        [HttpPost]
        public async Task Post([FromBody] Command value)
        {
            await _commandService.SaveAsync(value, value.PluginId);
        }

        

        /// PUT api/<CommandController>/5
        [HttpPut("{id}")]
        public async Task Put(long id, [FromBody] Command value, CancellationToken token)
        {
        }

        /// DELETE api/<CommandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
