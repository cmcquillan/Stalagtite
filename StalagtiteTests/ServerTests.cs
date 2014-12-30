using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stalagtite;
using Stalagtite.IO;

namespace StalagtiteTests
{
    [TestFixture]
    public class ServerTests
    {
        public const int TEST_SPEED = 9001;

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterListenerThrowsOnNull()
        {
            var server = new ClientManager(new GameState());
            server.RegisterClientListener(null);
        }

        [Test]
        public void RunOverloadIncrementsByAppropriateTicks()
        {
            var server = new StagServer();
            server.Speed = TEST_SPEED;
            server.Run(1L);
            Assert.AreEqual(1, server.LiveTicks);
        }

        [Test]
        public void RunOverloadIncrementsByAppropriateTicksX10()
        {
            var server = new StagServer();
            server.Speed = TEST_SPEED;
            server.Run(10L);
            Assert.AreEqual(10, server.LiveTicks);
        }
    }
}
