using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Api.Controllers.Base;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;

namespace MinecraftApi.Api.Controllers.Integrations.Roles
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : CrudBaseController<Role>
    {
        /// <summary>
        /// Default
        /// </summary>
        /// <param name="repositoryService"></param>
        /// <param name="logger"></param>
        public RolesController(IRepositoryService<Role> repositoryService, ILogger<CrudBaseController<Role>> logger) : base(repositoryService, logger)
        {
        }
    }
}
