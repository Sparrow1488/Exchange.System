using Exchange.Server.Protocols.Receivers;
using NUnit.Framework;
using System;

namespace Exchange.Tests.Server
{
    public class ClientReceiverTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Create_ValidHostAndPort_InstanceOfClientReceiver()
        {
            var receiver = ClientReceiver.Create("127.0.0.1", 80);
            Assert.IsInstanceOf<ClientReceiver>(receiver);
        }

        [Test]
        public void Create_InvalidHostAndPort_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                ClientReceiver.Create("", 80));
        }

        [Test]
        public void Create_HostAndInvalidPort_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                ClientReceiver.Create("127.0.0.1", -1));
        }

        [Test]
        public void Create_InvalidHostAndInvalidPort_ThrowArgumentException()
        {
            string host = null;
            Assert.Throws<ArgumentException>(() =>
                ClientReceiver.Create(host, -1));
        }
    }
}