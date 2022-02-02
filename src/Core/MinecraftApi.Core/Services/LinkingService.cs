using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Services
{
    internal class LinkingService
    {
        private readonly IRepositoryService<MinecraftPlayer, string> playerRepository;
        private readonly IRepositoryService<LinkRequest> linkRequestRepository;
        public LinkingService(IRepositoryService<MinecraftPlayer, string> playerRepository, IRepositoryService<LinkRequest> linkRepository)
        {
            this.playerRepository = playerRepository;
            this.linkRequestRepository = linkRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<LinkRequest> CreateLinkRequestAsync(MinecraftPlayer player)
        {
            //check the player:
            if(player == null)
                throw new ArgumentNullException(nameof(player));
            if(string.IsNullOrEmpty(player.Id))
                throw new ArgumentNullException(nameof(player.Id));
            if(string.IsNullOrEmpty(player.PlayerName))
                throw new ArgumentNullException(nameof(player.PlayerName));
            
            await playerRepository.CreateOrUpdateAsync(player);
            var linkRequest = CreateLinkRequest(player);
            return await linkRequestRepository.CreateAsync(linkRequest);      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private LinkRequest CreateLinkRequest(MinecraftPlayer player)
        {
            var guid = Guid.NewGuid().ToString();
            return new LinkRequest
            {
                Player = player,
                RequestedTime = DateTime.UtcNow,
                UniqueId = guid,
                ExpirationTime = DateTime.UtcNow.AddHours(1),
                PlayerId = player.Id,
                Oauth2RequestUrl = $"?state={guid}"
            };
        }
    }
}
