using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Contracts.Services
{
    /// <summary>
    /// Generic repository service
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TIdType">The type of the ids of the entity type</typeparam>
    public interface IRepositoryService<TEntity, TIdType> where TEntity : IEntity<TIdType>
    {
        
        /// <summary>
        /// Gets an item. Returns null if not present.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> GetAsync(TIdType id);
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// Creates an item
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the created entity.</returns>
        Task<TEntity> CreateAsync(TEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> CreateOrUpdateAsync(TEntity entity);
        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TIdType id);
        /// <summary>
        /// Updates an item.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
    }
    /// <summary>
    /// Default repository service to be used with types that have an id long.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryService<TEntity> : IRepositoryService<TEntity, long>
        where TEntity : IEntity
    {
        
    }
}
