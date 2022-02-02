using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Configuration
{
    /// <summary>
    /// Api roles
    /// </summary>
    public class ApiRole
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ApiRole(string roleName)
        {
            RoleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
        }
        public ApiRole() { }
        public string? RoleName { get; set; }
        public string? RoleAdValue { get; set; }
    }
    /// <summary>
    /// Predefined roles
    /// </summary>
    public class PredifinedRoles
    {

        public const string AdminsPolicyName = "Admins";
        public ApiRole Admins { get; set; } = new ApiRole("Admin");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    }
}
