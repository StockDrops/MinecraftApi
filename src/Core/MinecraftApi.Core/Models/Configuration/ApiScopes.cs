using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStockApi.Core.Models.Configuration
{
    /// <summary>
    /// Scopes for the app
    /// </summary>
    public static class ApiScopes
    {
        /// <summary>
        /// Allows read operataions
        /// </summary>
        public const string Read = "read";
        /// <summary>
        /// Allows write operation
        /// </summary>
        public const string Write = "write";
        /// <summary>
        /// Allows delete
        /// </summary>
        public const string Delete = "delete";
        /// <summary>
        /// Allows updating.
        /// </summary>
        public const string Update = "update";
        /// <summary>
        /// Allows you to run commands.
        /// </summary>
        public const string Run = "run";
    }
}
