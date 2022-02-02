using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinecraftApi.Api.Contracts;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using OpenStockApi.Core.Models.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinecraftApi.Api.Controllers.Base
{
    ///<inheritdoc/>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CrudBaseController<T> : ControllerBase, ICrudController<T> where T : class, IEntity
    {
        /// <summary>
        /// Repository service
        /// </summary>
        protected IRepositoryService<T> _repositoryService;
        private readonly ILogger<CrudBaseController<T>> _logger;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="repositoryService"></param>
        /// <param name="logger"></param>
        public CrudBaseController(IRepositoryService<T> repositoryService, ILogger<CrudBaseController<T>> logger)
        {
            _repositoryService = repositoryService;
            _logger = logger;
        }
        // GET: api/<ValuesController>
        /// <summary>
        /// Gets all the items of this kind.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RequiredScope(ApiScopes.Read)]
        public virtual Task<List<T>> GetAsync()
        {
            return _repositoryService.GetAllAsync();
        }
        /// <summary>
        /// Get an item with its id.
        /// </summary>
        /// <param name="id">long describing the id of the item.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [RequiredScope(ApiScopes.Read)]
        public virtual Task<T?> GetAsync(long id)
        {
            return _repositoryService.GetAsync(id);
        }
        /// <summary>
        /// Save an item.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        //[Authorize(Policy = PredifinedRoles.AdminsPolicyName)]
        [HttpPost]
        [RequiredScope(ApiScopes.Write)]
        public virtual async Task<ActionResult<T>> SaveAsync([FromBody] T entity)
        {
            try
            {
                await _repositoryService.CreateAsync(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                //log
                _logger.LogError(ex, "");
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Deletes the item with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Policy = PredifinedRoles.AdminsPolicyName)]
        [HttpDelete("{id}")]
        [RequiredScope(ApiScopes.Delete)]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await _repositoryService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(ex);
            }
        }
        // PUT api/<ValuesController>/5
        /// <summary>
        /// Updates an item with a given id with the given json.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        //[Authorize(Policy = PredifinedRoles.AdminsPolicyName)]
        [HttpPut("{id}")]
        [RequiredScope(ApiScopes.Update)]
        public virtual async Task<IActionResult> UpdateAsync(long id, [FromBody] T entity)
        {
            if (entity == null)
                return BadRequest(new ArgumentNullException(nameof(entity)));
            if (id == 0)
                return BadRequest(new ArgumentException("id cannot be 0"));
            if (entity.Id != id)
                return BadRequest(new ArgumentException("id and entitie's id must be the same."));
            await _repositoryService.UpdateAsync(entity);
            return Ok();
        }
    }
}
