using Microsoft.Extensions.Logging;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Rcon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Services
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
        private readonly ILogger _logger;
        private readonly RconClientService _clientService;
        private readonly SemaphoreSlim _semaphore;
        private int requestNumber = 1; //0 is reserved for authentication.
        /// <summary>
        /// Constructor for the run command service. Use it with Dependency Injection.
        /// </summary>
        /// <param name="rconClient"></param>
        /// <param name="logger"></param>
        public RunCommandService(RconClientService rconClient, ILogger<RunCommandService> logger)
        {
            _clientService = rconClient;
            _logger = logger;
            _semaphore = new SemaphoreSlim(0, 1);
        }
        /// <summary>
        /// Runs a command asynchronously.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunCommandAsync(string command, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken); //RCON can only do one request at a time so we will make sure to only send one at a time.
            if(!_clientService.IsInitialized)
            {
                await _clientService.InitializeAsync(cancellationToken);
            }
            if (!_clientService.IsAuthenticated)
            {
                await _clientService.AuthenticateAsync(cancellationToken);
            }
            var rconCommand = new RconCommand(command, requestNumber);
            var response = await _clientService.SendMessageAsync(rconCommand, cancellationToken);
            _semaphore.Release();
            //The rest of the code doesn't use the rcon server, so we can release the semaphore and allow it to receive the next command.
        }
    }
}
