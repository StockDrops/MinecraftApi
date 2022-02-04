using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MinecraftApi.Core.Contracts.Services;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Minecraft.Players;
using MinecraftApi.Core.Services.Patreon;
using MinecraftApi.Ef.Models;
using MinecraftApi.Integrations.Patreon;
using System.Text;

namespace MinecraftBlazingHub.Services.Integrations
{
    /// <summary>
    /// 
    /// </summary>
    public class BlazorPatreonService : PatreonService, IBlazorPatreonService
    {
        private readonly PatreonServiceOptions options;
        private readonly NavigationManager navigationManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="client"></param>
        /// <param name="linkedPlayerRepositoryService"></param>
        /// <param name="minecraftPlayerRepositoryService"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="logger"></param>
        public BlazorPatreonService(IOptions<PatreonServiceOptions> options, 
            HttpClient client, 
            IRepositoryService<LinkedPlayer> linkedPlayerRepositoryService, 
            IRepositoryService<MinecraftPlayer, string> minecraftPlayerRepositoryService, 
            IDbContextFactory<PluginContext> dbContextFactory,
            ILogger<PatreonService> logger,
            NavigationManager navigationManager
            ) 
            : base(options, client, linkedPlayerRepositoryService, minecraftPlayerRepositoryService, dbContextFactory, logger)
        {
            this.navigationManager = navigationManager;
            this.options = options.Value;
        }

        public string GetOauth2Url(string requestId)
        {
            var builder = PatreonOauthUrlBuilder.CreateBuilder();
            builder.AddClientId(options.ClientId);
            builder.AddRedirectUrl(options.RedirectUrl);
            builder.AddScope(options.Scope);
            builder.AddState(requestId);
            return builder.Build();
        }
        public void RedirectUserToOauth2(string requestId)
        {
            var url = GetOauth2Url(requestId);
            navigationManager.NavigateTo(url);
        }
    }
    public class PatreonOauthUrlBuilder
    {
        private StringBuilder stringBuilder = new StringBuilder("https://www.patreon.com/oauth2/authorize");
        private QueryString query = QueryString.Create("response_type", "code");
        public static PatreonOauthUrlBuilder CreateBuilder()
        {
            return new PatreonOauthUrlBuilder();
        }
        public void AddClientId(string clientId)
        {
            query = query.Add("client_id", clientId);
            //stringBuilder.Append($"&client_id={clientId}");
        }
        public void AddRedirectUrl(string url)
        {
            query = query.Add("redirect_uri", url);
        }
        public void AddScope(string scope)
        {
            query = query.Add("scope", scope);
        }
        public void AddState(string state)
        {
            query = query.Add("state", state);
        }
        public string Build()
        {
            stringBuilder.Append(query.ToString());
            return stringBuilder.ToString();
        }
    }
}
