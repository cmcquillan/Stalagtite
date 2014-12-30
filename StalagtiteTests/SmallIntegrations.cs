using NUnit.Framework;
using Stalagtite;
using Stalagtite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class SmallIntegrations
    {
        [Test]
        public void RegisterClientListenerRunsActiveServer()
        {
            using (var listener = new TcpClientListener("localhost", 25025))
            {
                var server = new StagServer();
                server.Speed = ServerTests.TEST_SPEED;
                server.ClientManager.RegisterClientListener(listener);

                using (var client = new TcpClient("localhost", 25025))
                {
                    server.Run(100);
                    Assert.AreEqual(1, server.State.Players.Count);
                }
            }
        }

        //[Test]
        //public void ClientManagerProperlyDetectsDisconnects()
        //{
        //    using (var listener = new TcpClientListener("localhost", 25025))
        //    {
        //        var server = new StagServer();
        //        server.Speed = ServerTests.TEST_SPEED;
        //        server.ClientManager.RegisterClientListener(listener);

        //        using (var client = new TcpClient("localhost", 25025))
        //        {
        //            server.Run(10);
        //            client.Close();
        //        }
        //        Task.Delay(10000).Wait();
        //        server.Run(100);
        //        Assert.AreEqual(0, server.State.Players.Count);
        //    }
        //}
    }
}
