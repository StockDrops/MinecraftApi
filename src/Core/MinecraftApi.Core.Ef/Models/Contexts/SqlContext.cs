using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Models.Contexts
{
    ///<inheritdoc/>
    public class SqlContext : DbContext, IPluginContext
    {
        private readonly DatabaseConfigurationOptions _databaseConfiguration = new DatabaseConfigurationOptions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConfigurationOptions"></param>
        public SqlContext(IOptions<DatabaseConfigurationOptions> databaseConfigurationOptions)
        {
            _databaseConfiguration = databaseConfigurationOptions.Value;
        }
        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_databaseConfiguration.ConnectionString);
        }
        /////<inheritdoc/>
        //public SqlContext(DbContextOptions options) : base(options)
        //{
        //}
        /////<inheritdoc/>
        //protected SqlContext()
        //{
        //}
        /// <inheritdoc/>
        public DbSet<Plugin>? Plugins { get; set; }
        /// <inheritdoc/>
        public DbSet<Command>? Commands { get; set; }
        /// <inheritdoc/>
        public DbSet<Argument>? Arguments { get; set; }
    }
    /// <summary>
    /// Factory for creating migrations at design time.
    /// </summary>
    public class SqlContextContextFactory : IDesignTimeDbContextFactory<SqlContext>
    {
        ///<inheritdoc/>
        public SqlContext CreateDbContext(string[] args)
        {
            var options = new DatabaseConfigurationOptions
            {
                ConnectionString = "Server=db;Database=MinecraftApiMain",
                
            };
            return new SqlContext(Options.Create(options));
        }
    }
}
