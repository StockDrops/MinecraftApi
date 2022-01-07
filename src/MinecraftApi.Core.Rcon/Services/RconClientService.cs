using Microsoft.Extensions.Options;
using MinecraftApi.Core.Rcon.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Rcon.Services
{
    /// <summary>
    /// Class used for IOptions so that the class is usable with dependency injection.
    /// </summary>
    public class RconClientServiceOptions
    {
        /// <summary>
        /// Create a new options for the RconClientService.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public RconClientServiceOptions(string host, int port)
        {
            Host = host;
            Port = port;
        }
        /// <summary>
        /// Host to use with the connection
        /// </summary>
        public string Host { get; }
        /// <summary>
        /// Port to use with the connection.
        /// </summary>
        public int Port { get; }
    }
    /// <summary>
    /// Class to send RCON messages
    /// </summary>
    public class RconClientService
    {
        private TcpClient tcpClient;
        private RconClientServiceOptions options;
        /// <summary>
        /// Default constructor. Provide the connection information to be able to create the TcpClient
        /// </summary>
        /// <param name="host">Host used for the connection</param>
        /// <param name="port">Port used.</param>
        public RconClientService(string host, int port)
        {
            tcpClient = new TcpClient();
            options = new RconClientServiceOptions(host, port);
        }
        /// <summary>
        /// Constructor to make the RconClientService compatible with dependency injection.
        /// You can still use without by using the other available constructor.
        /// </summary>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RconClientService(IOptions<RconClientServiceOptions> options)
        {
            var host = options?.Value?.Host;
            var port = options?.Value?.Port;
            if (!string.IsNullOrWhiteSpace(host) && port != null)
            {
                tcpClient = new TcpClient();
                this.options = new RconClientServiceOptions(host, port.Value);
            }
            else
            {
                throw new ArgumentNullException($"{nameof(RconClientServiceOptions)} cannot be null, or the host, or the port!");
            }
        }
        /// <summary>
        /// Initializes the TCP connection. You MUST call this function before anything else. Or you will get an exception.
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync()
        {
            return tcpClient.ConnectAsync(options.Host, options.Port);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IRconMessage> SendMessageAsync(IRconMessage message, CancellationToken cancellationToken)
        {
            using (var networkStream = tcpClient.GetStream())
            {
                Memory<byte> memory = new Memory<byte>(new byte[1024]);
                await networkStream.WriteAsync(message.Body, cancellationToken);
                var bytesRead = await networkStream.ReadAsync(memory, cancellationToken);
                if(bytesRead > 0)
                {
                    return DecoderService.Decode(memory.ToArray());
                }
                throw new Exception("No Bytes Read");
            }
        }
    }
}
