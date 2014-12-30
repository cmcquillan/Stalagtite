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
    public class GameObjectTests
    {
        [Test]
        public void AddChildIncreasesCollectionSize()
        {
            var gs = new GameState();
            var parent = new World(gs);
            var mob = new MobileObject(gs);
            parent.AddChild(mob);

            Assert.AreEqual(1, parent.Children.Count());

        }

        [Test]
        public void RemoveChildDecreasesCollectionSize()
        {
            var gs = new GameState();
            var parent = new World(gs);
            var mob = new MobileObject(gs);
            parent.AddChild(mob);
            Assert.AreEqual(1, parent.Children.Count());
            parent.RemoveChild(mob);
            Assert.AreEqual(0, parent.Children.Count());
        }

        [Test]
        public void DestroyRemovesObjectFromParentCollection()
        {
            var gs = new GameState();
            var parent = new World(gs);
            var mob = new MobileObject(gs);
            parent.AddChild(mob);
            Assert.AreEqual(1, parent.Children.Count());
            mob.Destroy();
            Assert.AreEqual(0, parent.Children.Count());
        }

        [Test]
        public void AddChildRemovesObjectFromPreviousParentCollection()
        {
            var gs = new GameState();
            var oldParent = new World(gs);
            var newParent = new World(gs);
            var mob = new MobileObject(gs);

            oldParent.AddChild(mob);
            newParent.AddChild(mob);

            Assert.AreEqual(0, oldParent.Children.Count());
        }
    }
}
