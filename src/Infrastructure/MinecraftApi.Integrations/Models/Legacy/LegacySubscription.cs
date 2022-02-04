using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Models.Legacy
{
    /// <summary>
    /// Defines a subscription obtained from the legacy api
    /// </summary>
    public class LegacySubscription
    {
        /// <summary>
        /// Name of the subscription
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Alert limit if any
        /// </summary>
        public int AlertLimit { get; set; }
        /// <summary>
        /// Delay in milliseconds
        /// </summary>
        public int Delay { get; set; } //in milliseconds
    }
}
