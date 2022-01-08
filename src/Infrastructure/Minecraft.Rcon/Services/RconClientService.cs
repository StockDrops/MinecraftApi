using Microsoft.Extensions.Options;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Rcon.Services
{
    /// <summary>
    /// Class to send RCON messages
    /// </summary>
    public class RconClientService : IDisposable
    {
        private TcpClient tcpClient;
        private NetworkStream? networkStream;

        private RconClientServiceOptions options;
        
        /// <summary>
        /// Allows you to set the Password after initialization IF you set something wrong.
        /// </summary>
        public string Password
        {
            set 
            {
                options = new RconClientServiceOptions(options.Host, options.Port, value);
            }
        }

        /// <summary>
        /// Default constructor. Provide the connection information to be able to create the TcpClient
        /// </summary>
        /// <param name="host">Host used for the connection</param>
        /// <param name="port">Port used.</param>
        /// <param name="password">Password usedd in the connection.</param>
        public RconClientService(string host, int port, string password)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException("host");
            }
            tcpClient = new TcpClient();
            options = new RconClientServiceOptions(host, port, password);
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
            var password = options?.Value?.Password ?? string.Empty; //we allow for empty passwords since it's possible to have unsecure RCON connections.
            if (!string.IsNullOrWhiteSpace(host) && port != null)
            {
                tcpClient = new TcpClient();
                this.options = new RconClientServiceOptions(host, port.Value, password);
            }
            else
            {
                throw new ArgumentNullException($"{nameof(RconClientServiceOptions)} cannot be null, or the host, or the port!");
            }
        }
        /// <summary>
        /// Initializes the TCP connection. You MUST call this function before anything else. Or you will get an exception.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task InitializeAsync(CancellationToken token)
        {
            await tcpClient.ConnectAsync(options.Host, options.Port, token);
        }
        /// <summary>
        /// Authenticates the RCON connection.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> AuthenticateAsync(CancellationToken token)
        {
            var result = await SendMessageAsync(new RconMessage
            {
                Body = Encoding.ASCII.GetBytes(options.Password),
                RequestId = 0,
                Type = Core.Rcon.Models.RconMessageType.Authenticate
            }, token);
            if (result.RequestId == -1)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IRconMessage> SendMessageAsync(IRconMessage message, CancellationToken cancellationToken)
        {
            //We do not use a using here since we want to be able to reuse the TCP Client for as long as we need it. The class is IDisposable for safe disposing.
            networkStream = tcpClient.GetStream();
            Memory<byte> memory = new Memory<byte>(new byte[1024]);
            await networkStream.WriteAsync(message.RawMessage, cancellationToken);
            var bytesRead = await networkStream.ReadAsync(memory, cancellationToken);
            if(bytesRead > 0)
            {
                return DecoderService.Decode(memory.ToArray());
            }
            throw new Exception("No Bytes Read");
        }
        ///<inheritdoc/>
        public void Dispose()
        {
            networkStream?.Dispose();
            tcpClient?.Dispose();
        }
    }
}
