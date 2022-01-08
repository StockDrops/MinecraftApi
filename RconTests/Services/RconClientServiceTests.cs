using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Rcon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconUnitTests.Services
{
    [TestClass]
    public class RconClientServiceTests
    {
        [TestMethod]
        public void TestConstruction()
        {
            var emptyHostOptions = Microsoft.Extensions.Options.Options.Create<RconClientServiceOptions>(new RconClientServiceOptions("", 0, ""));
            Assert.ThrowsException<ArgumentNullException>(() => new RconClientService("", 0, ""));
            Assert.ThrowsException<ArgumentNullException>(() => new RconClientService(emptyHostOptions));
        }
    }
}
