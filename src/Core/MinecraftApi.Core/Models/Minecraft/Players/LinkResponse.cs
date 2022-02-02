using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models.Minecraft.Players
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkResponse
    {
        /// <summary>
        /// Is the request succesful?
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Message explaining the result.
        /// </summary>
        public string? Message { get; set; } 
    }
}
