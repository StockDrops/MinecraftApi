using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Models.Contexts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TOptions"></typeparam>
    public abstract class ContextFactory<TContext, TOptions> : IDbContextFactory<TContext>
        where TContext : DbContext
        where TOptions : DatabaseConfigurationOptions
    {
        /// <summary>
        /// The service provider
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// The options.
        /// </summary>
        protected readonly IOptions<TOptions> _options;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        public ContextFactory(IServiceProvider serviceProvider,
            IOptions<TOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
        }
        /// <inheritdoc/>
        public virtual TContext CreateDbContext()
        {
            switch (_options.Value.DatabaseType)
            {
                case DatabaseType.SqlServer:
                    return CreateSqlDbContext(_options);
                case DatabaseType.MySQL:
                    return CreateMySqlDbContext(_options);
                default:
                    throw new InvalidOperationException("Unknown type of database.");
            }
        }
        /// <summary>
        /// Creates a MySql context
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract TContext CreateMySqlDbContext(IOptions<TOptions> options);
        /// <summary>
        /// Creates an SQL context
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract TContext CreateSqlDbContext(IOptions<TOptions> options);
    }
    ///<inheritdoc/>
    public class PluginContextFactory : ContextFactory<PluginContext, DatabaseConfigurationOptions>, IDbContextFactory<PluginContext>
    {
        ///<inheritdoc/>
        public PluginContextFactory(IServiceProvider serviceProvider, IOptions<DatabaseConfigurationOptions> options) : base(serviceProvider, options)
        {
        }
        ///<inheritdoc/>
        protected override PluginContext CreateMySqlDbContext(IOptions<DatabaseConfigurationOptions> options)
        {
            return new MySqlContext(options);
        }
        /// <inheritdoc/>
        protected override PluginContext CreateSqlDbContext(IOptions<DatabaseConfigurationOptions> options)
        {
            return new SqlContext(options);
        }
    }
}
