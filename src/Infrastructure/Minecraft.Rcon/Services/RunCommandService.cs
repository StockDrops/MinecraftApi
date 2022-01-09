using Microsoft.Extensions.Logging;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Rcon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Rcon.Services
{
    /// <summary>
    /// Runs all commands.
    /// This class manages its own RconClient.
    /// It doesn't check for okness of commands.
    /// That is not its job. It's job is to manage the RconClient, and translate the answer into usable text.
    /// Make sure to use it as a singleton! You need one RunCommandService on your code only since you only have one rcon server.
    /// </summary>
    public class RunCommandService
    {
        private readonly ILogger? _logger;
        private readonly IRconClientService _clientService;
        private readonly SemaphoreSlim _semaphore;
        private int requestNumber = 0; //0 is reserved for authentication but we increment it always before sending a message so if we start at 0, the first
        //one will be 1, and so on.
        /// <summary>
        /// Constructor for the run command service. Use it with Dependency Injection.
        /// </summary>
        /// <param name="rconClient"></param>
        /// <param name="logger"></param>
        public RunCommandService(IRconClientService rconClient, ILogger<RunCommandService>? logger = null)
        {
            _clientService = rconClient;
            _logger = logger;
            _semaphore = new SemaphoreSlim(1);
        }
        /// <summary>
        /// Runs a command asynchronously.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IRconResponseMessage> RunCommandAsync(string command, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken); //RCON can only do one request at a time so we will make sure to only send one at a time.
            var localRequestId = Interlocked.Add(ref requestNumber, 1);
            if(!_clientService.IsInitialized)
            {
                await _clientService.InitializeAsync(cancellationToken);
            }
            if (!_clientService.IsAuthenticated)
            {
                await _clientService.AuthenticateAsync(cancellationToken);
            }
            var rconCommand = new RconCommand(command, localRequestId);
            var response = await _clientService.SendMessageAsync(rconCommand, cancellationToken);
            _semaphore.Release();
            //The rest of the code doesn't use the rcon server, so we can release the semaphore and allow it to receive the next command.
            
            if (response.Type == RconMessageType.Response)
            {
                return new RconResponseMessage(response.RequestId, response.Body ?? Array.Empty<byte>(), true);
            }
            return new RconResponseMessage(localRequestId, Array.Empty<byte>(), false);
        }
    }
}
