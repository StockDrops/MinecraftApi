using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Models
{
    /// <summary>
    /// Base db context for use in other specific infrastructure projects.
    /// </summary>
    public interface IPluginContext
    {
        /// <summary>
        /// Plugins for the EF Core base.
        /// </summary>
        public DbSet<Plugin>? Plugins { get; set; }
        /// <summary>
        /// Commands db set.
        /// </summary>
        public DbSet<Command>? Commands { get; set; }
        /// <summary>
        /// Arguments to store in the database
        /// </summary>
        public DbSet<Argument>? Arguments { get; set; }
    }
}
