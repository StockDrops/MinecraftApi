using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MinecraftApi.Core.Services.Patreon;
using System.Text;

namespace MinecraftBlazingHub.Services.Integrations
{
    /// <summary>
    /// 
    /// </summary>
    public class PatreonService : IPatreonService
    {
        private readonly PatreonServiceOptions options;
        private readonly NavigationManager navigationManager;
        private readonly HttpClient client;
        public PatreonService(IOptions<PatreonServiceOptions> patreonServiceOptions,
            HttpClient client,
            NavigationManager  navigationManager)
        {
            options = patreonServiceOptions.Value;
            this.client = client;
            this.navigationManager = navigationManager;
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
        public async Task VerifyCode(string code, string azureId, string requestId)
        {
            var endpoint = options.RequestUrl;
            var query = QueryString.Create("requestId", requestId).Add("externalId", azureId).Add("code", code);
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"{endpoint}{query}");
            var response = await client.SendAsync(request);

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
