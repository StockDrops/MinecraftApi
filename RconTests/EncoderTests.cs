using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Models;
using System.Text;

namespace RconTests
{
    [TestClass]
    public class EncoderTests
    {
        [TestMethod]
        public void CreateMessageTest()
        {
            var command = "test";
            RconMessage rconMessage = new RconMessage
            {
                RequestId = 1,
                Body = Encoding.ASCII.GetBytes(command),
                Type = RconMessageType.Command
            };

            byte[]? expectedrawmessage = new byte[]
            {
                14, 0, 0, 0, // Message length.
                1, 0, 0, 0, // Message Id
                2, 0, 0, 0, //Message Type
                116, 101, 115, 116,
                0,0, //Terminator
            };
            if(rconMessage.RawMessage != null)
            {
                 CollectionAssert.AreEqual(expectedrawmessage, rconMessage.RawMessage);
            }
            else
            {
                Assert.Fail("RawMessage cannot be null at this point");
            }
        }
    }
}