using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Models
{
    /// <summary>
    /// Options to be used with configuring the database
    /// </summary>
    public class DatabaseConfigurationOptions
    {
        /// <summary>
        /// Connection string to be used
        /// </summary>
        public string ConnectionString { get; set; } = "";
        /// <summary>
        /// Database type used.
        /// </summary>
        public DatabaseType DatabaseType { get; set; } = DatabaseType.SqlServer;
        /// <summary>
        /// MySql database version used with the api. Only for MySql.
        /// </summary>
        public string MySQLDatabaseVersion { get; set; } = "";
    }
    /// <summary>
    /// Database types
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// Microsoft's SQL server
        /// </summary>
        SqlServer,
        /// <summary>
        /// MySql
        /// </summary>
        MySQL,
        
    }
}
