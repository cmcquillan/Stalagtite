using NUnit.Framework;
using Stalagtite;
using Stalagtite.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class GameStateTests
    {
        [Test]
        public void GameObjectAddsItselfToObjectsList()
        {
            var gs = new GameState();
            Assert.AreEqual(gs.Game, gs.Objects[0]);
        }

        [Test]
        public void PlayerObjectAddsItselfToObjectMobsAndPlayersList()
        {
            var gs = new GameState();
            var player = new Player(gs, null);

            Assert.AreEqual(player, gs.Objects[1]);
            Assert.AreEqual(player, gs.Mobiles[0]);
            Assert.AreEqual(player, gs.Players[0]);
        }
    }
}
