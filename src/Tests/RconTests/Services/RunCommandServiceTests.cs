using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Contracts.Models;
using MinecraftApi.Core.Rcon.Contracts.Services;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Models;
using MinecraftApi.Rcon.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconUnitTests.Services
{
    [TestClass]
    public class RunCommandServiceTests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RunCommandServiceTest()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            CancellationToken cancellationToken = tokenSource.Token;
            await TestRunCommand(true, true, cancellationToken);
            await TestRunCommand(false, false, cancellationToken);
            await TestRunCommand(true, false, cancellationToken);
        }

        private async Task TestRunCommand(bool isInitialized, bool isAuthenticated, CancellationToken cancellationToken)
        {
            
            var mockedRconClientService = new Mock<IRconClientService>();
            mockedRconClientService.Setup(r => r.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            mockedRconClientService.Setup(r => r.AuthenticateAsync(It.IsAny<CancellationToken>()).Result).Returns(true);
            mockedRconClientService.Setup(r => r.SendMessageAsync(It.IsAny<IRconMessage>(), It.IsAny<CancellationToken>()).Result).Returns((IRconMessage message, CancellationToken token) => new RconMessage
            {
                RequestId = message.RequestId,
                Type = RconMessageType.Response,
                Body = message.Body,
            });
            mockedRconClientService.Setup(r => r.IsAuthenticated).Returns(isAuthenticated);
            mockedRconClientService.Setup(r => r.IsInitialized).Returns(isInitialized);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var runService = new RconCommandService(mockedRconClientService.Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            var response = await runService.RunCommandAsync("about", cancellationToken);
            mockedRconClientService.Verify(r => r.IsInitialized, Times.Once);
            mockedRconClientService.Verify(r => r.IsAuthenticated, Times.Once);

            mockedRconClientService.Verify(r => r.AuthenticateAsync(It.IsAny<CancellationToken>()), isAuthenticated ? Times.Never : Times.Once);
            mockedRconClientService.Verify(r => r.InitializeAsync(It.IsAny<CancellationToken>()), isInitialized ? Times.Never : Times.Once);
            mockedRconClientService.Verify(r => r.SendMessageAsync(It.IsAny<IRconMessage>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsTrue(response.IsSuccess);
        }
    }
}
