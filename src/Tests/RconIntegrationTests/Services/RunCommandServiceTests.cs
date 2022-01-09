using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Rcon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconIntegrationTests.Services
{
    [TestClass]
    public class RunCommandServiceTests
    {
        private RunCommandService service;
        private readonly IConfiguration Configuration;
        /// <summary>
        /// Construct
        /// </summary>
        public RunCommandServiceTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<RconClientServiceTests>();
            Configuration = builder.Build();

            var client = new RconClientService(Configuration["RconHost"], int.Parse(Configuration["RconPort"]), Configuration["RconPassword"]);
            service = new RunCommandService(client, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RunCommandServiceTest()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(1000));
            CancellationToken cancellationToken = tokenSource.Token;
            var responses = new List<Task<IRconResponseMessage>>();
            for(int i = 0; i < 100; i++)
            {
                responses.Add(TestRunCommand("list", cancellationToken));
            }
            await Task.WhenAll(responses);
            var count = 1;
            foreach(var response in responses)
            {
                var rawtext = Encoding.ASCII.GetString(response.Result.RawBody);
                var text = response.Result.Body;
                Assert.IsTrue(text.Contains("There are")); //The about command can give out the "checking version" which takes about 11 s to give out a response in
                //another text message outside of RCON. That command isn't great for this. so instead using list.
                Assert.IsTrue(response.Result.IsSuccess);
                Assert.IsTrue(response.Result.RequestId == count, "Request Id doesn't match {0} {1}", response.Result.RequestId, count);
                count++;
            }
        }

        private async Task<IRconResponseMessage> TestRunCommand(string command, CancellationToken cancellationToken)
        {
            return await service.RunCommandAsync(command, cancellationToken);
        }
    }
}
