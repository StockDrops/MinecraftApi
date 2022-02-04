using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace MinecraftApi.Core.Extensions
{
    /// <summary>
    /// Extensions for httpclient handlers.
    /// </summary>
    public static class HttpClientHandlerExtensions
    {
        /// <summary>
        /// Adds a client certificate
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static HttpClientHandler AddClientCertificate(this HttpClientHandler handler, X509Certificate2 certificate)
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(certificate);

            return handler;
        }
        /// <summary>
        /// This method will work on Windows, and on any other platform it will just be transparent, and do nothing.
        /// </summary>
        /// <param name="httpClientBuilder"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddClientCertificate(this IHttpClientBuilder httpClientBuilder, X509Certificate2 certificate)
        {
            httpClientBuilder.ConfigureHttpMessageHandlerBuilder(builder =>
            {
                if (builder.PrimaryHandler is HttpClientHandler handler)
                {
                    handler.AddClientCertificate(certificate);
                }
                else
                {
                    throw new InvalidOperationException($"Only {typeof(HttpClientHandler).FullName} handler type is supported. Actual type: {builder.PrimaryHandler.GetType().FullName}");
                }
            });

            return httpClientBuilder;
        }
    }
}
