using Microsoft.Identity.Web;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Integrations.Models.Legacy
{
    /// <summary>
    /// legacy api service.
    /// </summary>
    public class LegacyApiService
    {
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly HttpClient httpClient;
        public LegacyApiService(ITokenAcquisition tokenAcquisition,
            HttpClient httpClient)
        {
            this.tokenAcquisition = tokenAcquisition;
            this.httpClient = httpClient;
        }
        public async Task<LegacySubscription?> GetSubscription(string azureId)
        {
            string[] scopes = new string[] { "https://stockdrops.net/api/v1/read" }; //add this to the configuration.
            var accessToken = await tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/users/subscription/{azureId}");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LegacySubscription>().ConfigureAwait(false);
            }
            response.EnsureSuccessStatusCode();
            return null;
        }

    }
}
