using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftApi.Core.Rcon.Models;
using MinecraftApi.Core.Rcon.Services;
using System.Text;

namespace RconUnitTests
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
        [TestMethod]
        public void DecodeMessageTest()
        {
            byte[]? receivedMessage = new byte[]
            {
                14, 0, 0, 0, // Message length.
                1, 0, 0, 0, // Message Id
                2, 0, 0, 0, //Message Type
                116, 101, 115, 116,
                0,0, //Terminator
            };
            var message = DecoderService.Decode(receivedMessage);

            var command = "test";
            RconMessage rconMessage = new RconMessage
            {
                RequestId = 1,
                Body = Encoding.ASCII.GetBytes(command),
                Type = RconMessageType.Command
            };
            CollectionAssert.AreEqual(rconMessage.RawMessage, message.RawMessage);
            CollectionAssert.AreEqual(rconMessage.Header, message.Header);
            Assert.AreEqual(rconMessage.RequestId, message.RequestId);
            Assert.AreEqual(rconMessage.Type, message.Type);
        }
    }
}