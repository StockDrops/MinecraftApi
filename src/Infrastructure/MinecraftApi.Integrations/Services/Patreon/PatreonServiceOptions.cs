using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Services.Patreon
{
    /// <summary>
    /// The options required for using the patreon service.
    /// </summary>
    public class PatreonServiceOptions
    {
        /// <summary>
        /// Creates some options.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="codeAuthorizationEndpoint"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PatreonServiceOptions(string clientId, string clientSecret, string redirectUrl, string requestUrl, string codeAuthorizationEndpoint, string scope = "identity")
        {
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            RedirectUrl = redirectUrl ?? throw new ArgumentNullException(nameof(redirectUrl));
            CodeAuthorizationEndpoint = codeAuthorizationEndpoint ?? throw new ArgumentNullException(nameof(codeAuthorizationEndpoint));
            RequestUrl = requestUrl ?? throw new ArgumentNullException(nameof(requestUrl));
            Scope = scope;
        }
        /// <summary>
        /// Default constructor to avoid. only for automated frameworks.
        /// </summary>
        public PatreonServiceOptions() { }

        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId { get; set; } = null!;
        /// <summary>
        /// Client secret
        /// </summary>
        public string ClientSecret { get; set; } = null!;
        /// <summary>
        /// Redirect Url used with patreon.
        /// </summary>
        public string RedirectUrl { get; set; } = null!;
        /// <summary>
        /// Endpoint to use for verifying Oauth2 codes. By default we use "www.patreon.com/api/oauth2/token".
        /// </summary>
        public string CodeAuthorizationEndpoint { get; set; } = "www.patreon.com/api/oauth2/token";
        /// <summary>
        /// Url where to send the user to login with an external provider. Patreon should only be used as a "connect to" not a "login with". The request url should preserve the state query parameter.
        /// </summary>
        public string RequestUrl { get; set; } = null!;
        public string Scope { get; set; } = "identity";
    }
}
