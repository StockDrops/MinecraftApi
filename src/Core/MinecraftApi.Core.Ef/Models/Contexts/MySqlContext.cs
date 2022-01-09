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
    public class MySqlContext : PluginContext
    {
        ///<inheritdoc/>
        public MySqlContext(IOptions<DatabaseConfigurationOptions> databaseConfigurationOptions) : base(databaseConfigurationOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(_databaseConfiguration.ConnectionString, new MySqlServerVersion(_databaseConfiguration.MySQLDatabaseVersion));
        }
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
