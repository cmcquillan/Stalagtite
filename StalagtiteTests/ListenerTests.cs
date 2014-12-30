using NUnit.Framework;
using Stalagtite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class ListenerTests
    {
        [Test]
        public void TcpListenerRecognizesNewConnections()
        {
            using(var listener = new TcpClientListener("localhost", 25025))
            {
                using (var client = new TcpClient("localhost", 25025))
                {
                    Assert.IsTrue(listener.HasNewClients);
                }
            }
        }

        [Test]
        public void TcpListenerProvidesValidClient()
        {
            using(var listener = new TcpClientListener("localhost", 25025))
            {
                using(var client = new TcpClient("localhost", 25025))
                {
                    var endClient = listener.AcceptClient();
                    Assert.IsNotNull(endClient);
                    Assert.IsInstanceOf<TextReaderClient>(endClient);
                }
            }
        }
    }
}
