using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Describes a role in the minecraft service linked to a given subscription level
    /// </summary>
    public class Role : IEntity
    {
        ///<inheritdoc/>
        public long Id { get; set; }
        /// <summary>
        /// Level of the role as predefined in the code through the enum
        /// </summary>
        public RoleLevel Level { get; set; }
        /// <summary>
        /// The tier id
        /// </summary>
        public long? TierId { get; set; }

    }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum RoleLevel

    {
        Bronze = 0,
        Silver = 1,
        Gold = 2,
        Platinum = 3,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
