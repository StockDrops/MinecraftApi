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
    public class MySqlContext : DbContext, IPluginContext
    {
        private readonly DatabaseConfigurationOptions _databaseConfiguration = new DatabaseConfigurationOptions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConfigurationOptions"></param>
        public MySqlContext(IOptions<DatabaseConfigurationOptions> databaseConfigurationOptions)
        {
            _databaseConfiguration = databaseConfigurationOptions.Value;
        }
        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(_databaseConfiguration.ConnectionString, new MySqlServerVersion(_databaseConfiguration.MySQLDatabaseVersion));
        }

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
    public class MySqlContextContextFactory : IDesignTimeDbContextFactory<MySqlContext>
    {
        ///<inheritdoc/>
        public MySqlContext CreateDbContext(string[] args)
        {
            var options = new DatabaseConfigurationOptions
            {
                ConnectionString = "Server=db;Database=MinecraftApiMain",
                MySQLDatabaseVersion = "8.0.0"
            };
            return new MySqlContext(Options.Create<DatabaseConfigurationOptions>(options));
        }
    }
}
