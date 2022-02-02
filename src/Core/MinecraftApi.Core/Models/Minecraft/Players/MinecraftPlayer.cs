using MinecraftApi.Core.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Models
{
    /// <summary>
    /// Represents a minecraft player
    /// </summary>
    public class MinecraftPlayer : IStringEntity
    {
        /// <summary>
        /// Creates a player using a player name and the mc player id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerName"></param>
        /// /// <exception cref="ArgumentNullException"></exception>
        public MinecraftPlayer(string id, string playerName)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PlayerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
        }
        /// <summary>
        /// A unique id for the player used by minecraft
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string PlayerName { get; set; }
        
    }
}
