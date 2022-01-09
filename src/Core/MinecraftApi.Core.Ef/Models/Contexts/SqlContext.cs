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
    public class SqlContext : PluginContext
    {
        ///<inherit/>
        public SqlContext(IOptions<DatabaseConfigurationOptions> databaseConfigurationOptions) : base(databaseConfigurationOptions)
        {
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_databaseConfiguration.ConnectionString);
        }
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
