using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Api.Controllers.Base;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
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
    [Authorize]
    public class PluginController : CrudBaseController<Plugin>
    {
        /// <summary>
        /// Dfault constructor
        /// </summary>
        /// <param name="repositoryService"></param>
        /// <param name="logger"></param>
        public PluginController(IRepositoryService<Plugin> repositoryService, ILogger<CrudBaseController<Plugin>> logger) : base(repositoryService, logger)
        {
        }
    }
}
