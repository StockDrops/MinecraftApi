using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Api.Contracts.Models;
using MinecraftApi.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Services
{
    /// <summary>
    /// A generic crud service implementing an IRepositoryService
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId">Type of the id.</typeparam>
    public class CrudService<TContext, TEntity, TId> : IRepositoryService<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity<TId>
    {
        /// <summary>
        /// Context factory
        /// </summary>
        public IDbContextFactory<TContext> _dbContextFactory;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public CrudService(IDbContextFactory<TContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        ///<inheritdoc/>
        public virtual async Task<TEntity?> GetAsync(TId id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await GetAsync(id, context).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets an item using a context
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TEntity?> GetAsync(TId id, TContext context)
        {
            if (id != null && !id.Equals(default(TId)))
                return await context.Set<TEntity>().Where(x => x.Id != null && x.Id.Equals(id)).FirstOrDefaultAsync().ConfigureAwait(false);
            throw new ArgumentNullException(nameof(id));
        }
        ///<inheritdoc/>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }
        ///<inheritdoc/>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        ///<inheritdoc/>
        public virtual async Task DeleteAsync(TId id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var originalEntity = await GetAsync(id, context);
            if (originalEntity != null)
            {
                context.Set<TEntity>().Remove(originalEntity);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
        ///<inheritdoc/>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            if (entity.Id != null && !entity.Id.Equals(default(TId)))
            {
                var originalEntity = await GetAsync(entity.Id, context);
                if (originalEntity != null)
                {
                    context.Entry(originalEntity).CurrentValues.SetValues(entity);
                    await context.SaveChangesAsync();
                }
                else
                    throw new ArgumentOutOfRangeException(nameof(entity), "Entity is not found in the database. Cannot update it.");
            }
        }
        ///<inheritdoc/>
        public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            //using var context = await _dbContextFactory.CreateDbContextAsync();
            if (entity.Id != null && !entity.Id.Equals(default(TId)))
            {
                try
                {
                    await UpdateAsync(entity);
                    return entity;
                }
                catch (ArgumentOutOfRangeException) { }//the item doesn't exist so it must be created so carry on.
            }
            //the entity doesn't have an id.
            //we should try to create it
            return await CreateAsync(entity).ConfigureAwait(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class CrudService<TContext, TEntity> : CrudService<TContext, TEntity, long>, IRepositoryService<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public CrudService(IDbContextFactory<TContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
