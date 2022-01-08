using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Core.Rcon.Services;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconIntegrationTests
{
    [TestClass]
    public class RconClientServiceTests : IDisposable
    {
        IConfiguration Configuration { get; set; }
        private RconClientService rconClient;

        public RconClientServiceTests()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<RconClientServiceTests>();
            Configuration = builder.Build();
            rconClient = CreateService();
        }
        private RconClientService CreateService()
        {
            return new RconClientService(Configuration["RconHost"], int.Parse(Configuration["RconPort"]), Configuration["RconPassword"]);
        }

        /// <summary>
        /// Sends a raw command to the RCON connection for testing purposes.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TestSendingTestRawMessage()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000); //a 10s timeout
            var command = "kill Chinss";
            var message = new RconMessage
            {
                Body = Encoding.ASCII.GetBytes(command),
                RequestId = 1,
                Type = RconMessageType.Command
            };
            await rconClient.InitializeAsync(tokenSource.Token);
            await rconClient.AuthenticateAsync(tokenSource.Token);
            var result = await rconClient.SendMessageAsync(message, tokenSource.Token);
            if(result.RequestId == 1)
            {

            }
        }        
        /// <summary>
        /// Tests the connection to RCON, i.e. just the initialization of the TCP connection
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TestConnectingToRcon() 
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000); //a 10s timeout
            var rconClient = new RconClientService(Configuration["RconHost"], int.Parse(Configuration["RconPort"]), Configuration["RconPassword"]);
            await rconClient.InitializeAsync(tokenSource.Token);
        }
        /// <summary>
        /// Tests authentication to RCON, 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TestAuthenticationToRconAsync()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000); //a 10s timeout
            //intialize it:
            await rconClient.InitializeAsync(tokenSource.Token);
            //we login using the correct password passed throught the secrets.
            Assert.IsTrue(await rconClient.AuthenticateAsync(tokenSource.Token));
            //we change the password to random bs.
            rconClient.Password = "sdasda"; 
            Assert.IsFalse(await rconClient.AuthenticateAsync(tokenSource.Token));
            
        }

        public void Dispose()
        {
            rconClient?.Dispose();
        }
    }
}