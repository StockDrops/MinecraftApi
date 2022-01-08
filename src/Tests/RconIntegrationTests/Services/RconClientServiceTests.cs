using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Services;
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
            var command = "gm c lordhenry85";
            var message = new RconMessage
            {
                Body = Encoding.ASCII.GetBytes(command),
                RequestId = 1,
                Type = RconMessageType.Command
            };
            var result = await SendRawMessage(message, tokenSource.Token);
            Assert.IsTrue(result.RequestId == 1);
            Assert.IsTrue(result.Type == RconMessageType.Response);
        }
        /// <summary>
        /// Tests sending a text message.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestSendingTextMessage()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000); //a 10s timeout
            var command = "gm c lordhenry85";
            var textMessage = new RconTextMessage(command, 1, RconMessageType.Command);
            var result = await SendRawMessage(textMessage, tokenSource.Token);
            Assert.IsTrue(result.RequestId == 1);
            Assert.IsTrue(result.Type == RconMessageType.Response);
        }
        [TestMethod]
        public async Task TestSendingCommandMessage()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(10000);
            var command = new RconCommand("kill Chinss", 1);
            var result = await SendRawMessage(command, tokenSource.Token);
            Assert.IsTrue(result.RequestId == 1);
            Assert.IsTrue(result.Type == RconMessageType.Response);
        }
        private async Task<IRconMessage> SendRawMessage(IRconMessage message, CancellationToken token)
        {
            await rconClient.InitializeAsync(token);
            await rconClient.AuthenticateAsync(token);
            return await rconClient.SendMessageAsync(message, token);
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