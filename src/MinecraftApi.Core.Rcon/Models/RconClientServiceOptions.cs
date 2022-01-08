using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Models
{
    /// <summary>
    /// Class used for IOptions so that the class is usable with dependency injection.
    /// </summary>
    public class RconClientServiceOptions
    {
        /// <summary>
        /// Create a new options for the RconClientService.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public RconClientServiceOptions(string host, int port, string password)
        {
            Host = host;
            Port = port;
            Password = password;
        }
        /// <summary>
        /// Host to use with the connection
        /// </summary>
        public string Host { get; }
        /// <summary>
        /// Port to use with the connection.
        /// </summary>
        public int Port { get; }
        /// <summary>
        /// Password used in the RCON connection.
        /// </summary>
        public string Password { get; }
    }
}
