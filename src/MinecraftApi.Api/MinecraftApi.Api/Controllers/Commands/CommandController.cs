using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Ef.Services;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Ef.Models;
using Microsoft.EntityFrameworkCore;
using MinecraftApi.Api.Controllers.Base;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinecraftApi.Api.Controllers
{
    /// <summary>
    /// Manages reception of commands.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : CrudBaseController<Command>
    {
        /// <summary>
        /// default
        /// </summary>
        /// <param name="repositoryService"></param>
        /// <param name="logger"></param>
        public CommandController(IRepositoryService<Command> repositoryService, ILogger<CrudBaseController<Command>> logger) : base(repositoryService, logger)
        {
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
    }
}
