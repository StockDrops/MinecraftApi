using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Services;
using System.Threading.Tasks;

namespace RconIntegrationTests
{
    [TestClass]
    public class RconClientServiceTests
    {
        IConfiguration Configuration { get; set; }

        public RconClientServiceTests()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<RconClientServiceTests>();
            Configuration = builder.Build();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void TestSendingMessages()
        {
            var host = Configuration["RconHost"];
            var port = Configuration["RconPort"];
            var password = Configuration["RconPassword"];
        }
        public void TestLoginToRcon()
        {

        }
        /// <summary>
        /// Tests the connection to RCON, i.e. just the initialization of the TCP connection.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public async Task TestConnectingToRcon() 
        {
            using var rconClient = new RconClientService(Configuration["RconHost"], int.Parse(Configuration["RconPort"]), Configuration["RconPassword"]);
            await rconClient.InitializeAsync();
        }
    }
}