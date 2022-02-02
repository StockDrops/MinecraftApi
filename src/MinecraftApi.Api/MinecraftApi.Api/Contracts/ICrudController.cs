using Microsoft.AspNetCore.Mvc;
using MinecraftApi.Core.Api.Contracts.Models;

namespace MinecraftApi.Api.Contracts;
/// <summary>
/// Interface for a default crud controller
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrudController<T> where T : IEntity
{
    /// <summary>
    /// Gets all the items.
    /// </summary>
    /// <returns></returns>
    public Task<List<T>> GetAsync();
    /// <summary>
    /// Get a single item by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<T?> GetAsync(long id);
    /// <summary>
    /// Save an item.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Returns the created entity with id.</returns>
    public Task<ActionResult<T>> SaveAsync(T entity);
    /// <summary>
    /// Delete an item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<IActionResult> DeleteAsync(long id);
    /// <summary>
    /// Updates an item by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<IActionResult> UpdateAsync(long id, T entity);
}
